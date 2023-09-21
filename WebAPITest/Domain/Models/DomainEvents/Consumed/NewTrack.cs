using WebAPITest.Domain.Models.DomainEvents;

namespace WebAPITest.Domain.Models.DomainEvents.Consumed
{
    public class NewTrack : ConsumedEvent
    {
        private NewTrack()
        {

        }
        public NewTrack(DateTime eventDateTime, string name) : base(ConsumedEventTypes.GetByCode(ConsumedEventTypesEnum.NewTrack), eventDateTime)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}