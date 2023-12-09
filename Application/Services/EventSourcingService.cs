using Application.Interfaces;
using Domain.DomainEvents;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Application.Services
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
