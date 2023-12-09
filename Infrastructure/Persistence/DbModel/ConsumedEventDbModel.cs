using System.Text.Json;
using Domain.DomainEvents;
using Domain.DomainEvents.Consumed;

namespace Infrastructure.Persistence.DbModel
{
    public class ConsumedEventDbModel
    {
            #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ConsumedEventDbModel()
            #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            
        }
        public ConsumedEventDbModel( 
            ConsumedEventType type, 
            DateTime eventDateTime, 
            DateTime recieveDateTime, 
            ConsumedEventState state, 
            DateTime? processedDateTime, 
            string data)
        {
            Type = type;
            EventDateTime = eventDateTime;
            RecieveDateTime = recieveDateTime;
            State = state;
            ProcessedDateTime = processedDateTime;
            Data = data;
        }

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

            var consumedEventForDb = new ConsumedEventDbModel(
                consumedEvent.Type,
                consumedEvent.EventDateTime,
                consumedEvent.RecieveDateTime,
                consumedEvent.State,
                consumedEvent.ProcessedDateTime,
                data);
            return consumedEventForDb;
        }

        public ConsumedEvent GetDomainEntity()
        {
            ConsumedEvent consumedEvent;
            switch (Type.Code)
            {
                //почему-то так не работает
                //case ConsumedEventTypesEnum.NewTrack.ToString():
                case "NewTrack":
                    consumedEvent = JsonSerializer.Deserialize<NewTrack>(Data)!;
                    break;
                case "SeasonCalendarPublished":
                    consumedEvent = JsonSerializer.Deserialize<SeasonCalendarPublished>(Data)!;
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
