using Domain.DomainEvents;

namespace Domain.DomainEvents.Consumed
{
    public class NewTrack : ConsumedEvent
    {
        //private NewTrack()
        //{

        //}
        public NewTrack(DateTime eventDateTime, string name) : base(ConsumedEvent.GetTypeByCode(ConsumedEventTypesEnum.NewTrack), eventDateTime)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}