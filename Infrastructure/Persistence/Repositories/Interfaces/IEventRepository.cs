using Domain.DomainEvents;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IEventRepository
    {
        void SaveEventAndSetNewerEventsToRepeatProcess(ConsumedEvent consumedEvent);
        ConsumedEvent TakeOldestEventToProcessAndSetProcessingState(out bool reProcessing);
        void UpdateEvent(ConsumedEvent consumedEvent);
    }
}