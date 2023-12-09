using Domain.DomainEvents;

namespace Domain
{
    public static class DomainConstants
    {
        public static readonly List<ConsumedEventState> toProcessConsumedEventStates = new List<ConsumedEventState>
        {
            ConsumedEvent.GetStateByCode(ConsumedEventStatesEnum.Recieved),
            ConsumedEvent.GetStateByCode(ConsumedEventStatesEnum.ToRepeatProcess)
        };
    }
}