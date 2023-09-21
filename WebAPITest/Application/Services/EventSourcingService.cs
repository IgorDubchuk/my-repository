using WebAPITest.Application.Interfaces;
using WebAPITest.Domain.Models.DomainEvents;
using WebAPITest.Infrastructure.Persistence.Repositories.Interfaces;

namespace WebAPITest.Application.Services
{
    public class EventSourcingService : IEventSourcingService
    {
        private readonly IEventRepository _eventRepository;

        public EventSourcingService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public ConsumedEvent SaveConsumedEvent(ConsumedEvent consumedEvent)
        {
            //Сохраняем событие, и, если есть более поздние события, то им проставляем статус "к повторной обработке"

            _eventRepository.SaveEventAndSetNewerEventsToRepeatProcess(consumedEvent);
            return consumedEvent;
        }
    }
}
