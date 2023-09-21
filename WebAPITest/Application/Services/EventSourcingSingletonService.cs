using WebAPITest.Application.Interfaces;
using WebAPITest.Domain.Interfaces;
using WebAPITest.Domain.Models.DomainEvents;
using WebAPITest.Domain.Models.DomainEvents.Consumed;
using WebAPITest.Infrastructure.Persistence;
using WebAPITest.Infrastructure.Persistence.Repositories.Interfaces;

namespace WebAPITest.Application.Services
{
    public class EventSourcingSingletonService : IEventSourcingSingletonService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private bool IsProcessing;

        public EventSourcingSingletonService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ProcessConsumedEvents()
        {
            if (IsProcessing) { throw new ApplicationException("Unable to start new event processing due to existing active same process."); }

            IsProcessing = true;


            //Processing in new thread for fast API answer
            Task processingTask = new(() =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var _dbContext = scope.ServiceProvider.GetRequiredService<F1DbContext>();
                var _eventRepository = scope.ServiceProvider.GetRequiredService<IEventRepository>();
                var _eventService = scope.ServiceProvider.GetRequiredService<IEventService>();

                while (true)
                {
                    ConsumedEvent eventToProcess = null;
                    try { eventToProcess = _eventRepository.TakeOldestEventToProcessAndSetProcessingState(); }
                    catch (ApplicationException) { break; }

                    //Есть более оптимальное решение?
                    var eventType = eventToProcess!.GetType().ToString();

                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            switch (eventType)
                            {
                                case "WebAPITest.Models.DomainEvents.Consumed.NewTrack":
                                    _eventService.ProcessEvent((NewTrack)eventToProcess);
                                    break;

                                case "WebAPITest.Models.DomainEvents.Consumed.SeasonCalendarPublished":
                                    _eventService.ProcessEvent((SeasonCalendarPublished)eventToProcess);
                                    break;

                                default: throw new ApplicationException("Can not process consumed event: unsupported event type.");
                            }

                            eventToProcess.SetProcessed();
                            _eventRepository.UpdateEvent(eventToProcess);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            eventToProcess.SetError();
                            _eventRepository.UpdateEvent(eventToProcess);
                        }
                    }
                }
                IsProcessing = false;

            });
            processingTask.Start();

            //добавить отлов ошибок и освобождение IsProcessing
        }
    }
}
