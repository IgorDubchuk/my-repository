using Application.Interfaces;
using Domain.DomainEvents;
using Domain.DomainEvents.Consumed;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class EventSourcingSingletonService : IEventSourcingSingletonService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<EventSourcingSingletonService> _logger;
        public EventSourcingSingletonService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<EventSourcingSingletonService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }
        private bool IsProcessing;

        public void ProcessConsumedEvents()
        {
            if (IsProcessing) { throw new ApplicationException("Unable to start new event processing due to existing active same process."); }
            _logger.LogInformation("Event processing has started");

            IsProcessing = true;

            //Processing in new thread for fast API answer
            Task processingTask = new(() =>
            {
                try
                {
                    var throwError = true;
                    while (true)
                    {                        
                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var _eventService = scope.ServiceProvider.GetRequiredService<IEventService>();
                            var _baseRepository = scope.ServiceProvider.GetRequiredService<IBaseRepository>();
                            var _eventRepository = scope.ServiceProvider.GetRequiredService<IEventRepository>();

                            ConsumedEvent eventToProcess;
                            bool reProcessing;
                            try { eventToProcess = _eventRepository.TakeOldestEventToProcessAndSetProcessingState(out reProcessing); }
                            catch (ApplicationException) { break; }

                            var eventType = eventToProcess!.GetType().Name;

                            try
                            {
                                switch (eventType)
                                {
                                    case "NewTrack":
                                        _eventService.ProcessEvent((NewTrack)eventToProcess, reProcessing);
                                        break;

                                    case "SeasonCalendarPublished":
                                        _eventService.ProcessEvent((SeasonCalendarPublished)eventToProcess, reProcessing);
                                        break;

                                    default: throw new ApplicationException("Can not process consumed event: unsupported event type.");
                                }                                
                                eventToProcess.SetProcessed();
                            }
                            catch (Exception ex)
                            {
                                eventToProcess.SetError();
                                _logger.LogError(ex, $"Error during consumed event processing. EventID: {eventToProcess.Id} ({ex.Message})");
                                //if we'll need to interrupt the processing events in case of one event failure, we should add "break;" here
                            }
                            finally 
                            { 
                                _eventRepository.UpdateEvent(eventToProcess);
                                if (throwError)
                                {
                                    throwError = false;
                                }
                                else
                                    _baseRepository.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during event processing");
                }
                finally
                {
                    IsProcessing = false;
                }
            });
            processingTask.Start();
        }
    }
}
