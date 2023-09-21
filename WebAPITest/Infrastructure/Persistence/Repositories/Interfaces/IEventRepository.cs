using WebAPITest.Domain.Models.DomainEvents;

namespace WebAPITest.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IEventRepository
    {
        void SaveEventAndSetNewerEventsToRepeatProcess(ConsumedEvent consumedEvent);
        ConsumedEvent TakeOldestEventToProcessAndSetProcessingState();
        void UpdateEvent(ConsumedEvent consumedEvent);
    }
}