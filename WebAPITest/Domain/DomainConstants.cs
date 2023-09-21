using WebAPITest.Domain.Models.DomainEvents;

namespace WebAPITest.Domain
{
    public static class DomainConstants
    {
        public static readonly List<ConsumedEventState> toProcessConsumedEventStates = new List<ConsumedEventState>
        {
            ConsumedEventStates.GetByCode(ConsumedEventStatesEnum.Recieved),
            ConsumedEventStates.GetByCode(ConsumedEventStatesEnum.ToRepeatProcess)
        };
    }
}