using System.Text.Json;
using WebAPITest.Domain.Models.DomainEvents;
using WebAPITest.Domain.Models.DomainEvents.Consumed;

namespace WebAPITest.Infrastructure.Persistence.DbModel
{
    public class ConsumedEventDbModel
    {
        public int Id { get; set; }
        public ConsumedEventType Type { get; set; }
        public DateTime EventDateTime { get; set; }
        public DateTime RecieveDateTime { get; set; }
        public ConsumedEventState State { get; set; }
        public DateTime? ProcessedDateTime { get; set; }
        public string Data { get; set; }

        public static ConsumedEventDbModel CreateFromDomainEntity(ConsumedEvent consumedEvent)
        {
            string data = JsonSerializer.Serialize(
            consumedEvent,
            consumedEvent.GetType(),
            new JsonSerializerOptions { WriteIndented = true });

            var consumedEventForDb = new ConsumedEventDbModel()
            {
                Type = consumedEvent.Type,
                EventDateTime = consumedEvent.EventDateTime,
                RecieveDateTime = consumedEvent.RecieveDateTime,
                State = consumedEvent.State,
                ProcessedDateTime = consumedEvent.ProcessedDateTime,
                Data = data
            };
            return consumedEventForDb;
        }

        public ConsumedEvent GetDomainEntity()
        {
            ConsumedEvent consumedEvent = null;
            switch (Type.Code)
            {
                //почему-то так не работает
                //case ConsumedEventTypesEnum.NewTrack.ToString():
                case "NewTrack":
                    consumedEvent = JsonSerializer.Deserialize<NewTrack>(Data);
                    break;
                case "SeasonCalendarPublished":
                    consumedEvent = JsonSerializer.Deserialize<SeasonCalendarPublished>(Data);
                    break;
                default: throw new ArgumentException("Can not deserialize JSON. Unsupported event type");
            }
            consumedEvent.Id = Id;
            consumedEvent.Type = Type;
            consumedEvent.EventDateTime = EventDateTime;
            consumedEvent.RecieveDateTime = RecieveDateTime;
            consumedEvent.State = State;
            consumedEvent.ProcessedDateTime = ProcessedDateTime;
            return consumedEvent;
        }
    }
}
