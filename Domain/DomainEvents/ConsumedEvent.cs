//using Newtonsoft.Json.Linq;
//using System.CodeDom;
//using System.Diagnostics;
using System.Text.Json.Serialization;
//using Domain;

namespace Domain.DomainEvents
{
    public class ConsumedEvent
    {
            #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ConsumedEvent()
            #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        public ConsumedEvent(ConsumedEventType type, DateTime eventDateTime)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            EventDateTime = eventDateTime;
            RecieveDateTime = DateTime.Now;
            State = ConsumedEvent.States.Single(s => s.Code == "Recieved");
        }

        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public ConsumedEventType Type { get; set; }
        [JsonIgnore]
        public DateTime EventDateTime { get; set; }
        [JsonIgnore]
        public DateTime RecieveDateTime { get; set; }
        [JsonIgnore]
        public ConsumedEventState State { get; set; }
        [JsonIgnore]
        public DateTime? ProcessedDateTime { get; set; }


        public void SetProcessed()
        {
            State = GetStateByCode(ConsumedEventStatesEnum.Processed);
            ProcessedDateTime = DateTime.Now;
        }

        public void SetError()
        {
            State = GetStateByCode(ConsumedEventStatesEnum.Error);
            ProcessedDateTime = DateTime.Now;
        }


        public static List<ConsumedEventType> Types = new()
        {
            new ConsumedEventType (1, ConsumedEventTypesEnum.SeasonCalendarPublished.ToString(), "Опубликован календарь сезона"),
            new ConsumedEventType (2, ConsumedEventTypesEnum.SeasonParticipantsPublished.ToString(), "Опубликован состав команд-участников сезона" ),
            new ConsumedEventType (3, ConsumedEventTypesEnum.DriverContractSigned.ToString(), "Заключен контракт с гонщиком" ),
            new ConsumedEventType (4, ConsumedEventTypesEnum.RaceFinished.ToString(), "Гонка завершилась" ),
            new ConsumedEventType (5, ConsumedEventTypesEnum.NewTrack.ToString(), "Новый автодром" )
        };
        public static ConsumedEventType GetTypeByCode(ConsumedEventTypesEnum code)
        {
            return Types.Single(v => v.Code == code.ToString());
        }

        public static List<ConsumedEventState> States = new()
        {
            new ConsumedEventState(1, ConsumedEventStatesEnum.Recieved.ToString(), "Событие получено"),
            new ConsumedEventState(2, ConsumedEventStatesEnum.Processed.ToString(), "Событие обработано"),
            new ConsumedEventState(3, ConsumedEventStatesEnum.ToRepeatProcess.ToString(), "Событие должно быть обработано повторно"),
            new ConsumedEventState(4, ConsumedEventStatesEnum.Processing.ToString(), "Событие обрабатывается"),
            new ConsumedEventState(5, ConsumedEventStatesEnum.Error.ToString(), "При обработке события произошла ошибка")
        };
        public static ConsumedEventState GetStateByCode(ConsumedEventStatesEnum code)
        {
            return States.Single(v => v.Code == code.ToString());
        }
    }

    public class ConsumedEventType : DomainDictionaryEntry, IDomainDictionary
    {
        public ConsumedEventType(int id, string code, string name) : base(id, code, name) { }
    }
    public enum ConsumedEventTypesEnum
    {
        SeasonCalendarPublished,
        SeasonParticipantsPublished,
        DriverContractSigned,
        RaceFinished,
        NewTrack
    }

    public class ConsumedEventState : DomainDictionaryEntry, IDomainDictionary
    {
        public ConsumedEventState(int id, string code, string name) : base(id, code, name) { }
    }
    public enum ConsumedEventStatesEnum
    {
        Recieved,
        Processed,
        ToRepeatProcess,
        Processing,
        Error
    }
}
