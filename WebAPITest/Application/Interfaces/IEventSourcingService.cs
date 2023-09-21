using WebAPITest.Domain.Models.DomainEvents;

namespace WebAPITest.Application.Interfaces
{
    public interface IEventSourcingService
    {
        ConsumedEvent SaveConsumedEvent(ConsumedEvent consumedEvent);
    }
}