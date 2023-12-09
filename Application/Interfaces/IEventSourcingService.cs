using Domain.DomainEvents;

namespace Application.Interfaces
{
    public interface IEventSourcingService
    {
        ConsumedEvent SaveConsumedEvent(ConsumedEvent consumedEvent);
    }
}