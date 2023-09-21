using System.CodeDom;
using System.Diagnostics;
using System.Text.Json.Serialization;
using WebAPITest.Domain.Models;

namespace WebAPITest.Domain.Models.DomainEvents
{
    public class ConsumedEvent
    {
        //пришлось его сделать пабликом, иначе не получилось доставать из JSON денормализованное событие, то же касается сеттеров
        //мне это не нравится, так как модель не защищенная. Есть ли способ сделать ее защищенной?
        public ConsumedEvent()
        {

        }
        public ConsumedEvent(ConsumedEventType type, DateTime eventDateTime)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            EventDateTime = eventDateTime;
            RecieveDateTime = DateTime.Now;
            State = ConsumedEventStates.Values.Single(s => s.Code == "Recieved");
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
            State = ConsumedEventStates.GetByCode(ConsumedEventStatesEnum.Processed);
            ProcessedDateTime = DateTime.Now;
        }

        public void SetError()
        {
            State = ConsumedEventStates.GetByCode(ConsumedEventStatesEnum.Error);
            ProcessedDateTime = DateTime.Now;
        }
    }



    public class ConsumedEventType : DomainDictionaryEntry
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
    public static class ConsumedEventTypes
    {
        public static new List<ConsumedEventType> Values = new()
        {
            new ConsumedEventType (1, ConsumedEventTypesEnum.SeasonCalendarPublished.ToString(), "Опубликован календарь сезона"),
            new ConsumedEventType (2, ConsumedEventTypesEnum.SeasonParticipantsPublished.ToString(), "Опубликован состав команд-участников сезона" ),
            new ConsumedEventType (3, ConsumedEventTypesEnum.DriverContractSigned.ToString(), "Заключен контракт с гонщиком" ),
            new ConsumedEventType (4, ConsumedEventTypesEnum.RaceFinished.ToString(), "Гонка завершилась" ),
            new ConsumedEventType (5, ConsumedEventTypesEnum.NewTrack.ToString(), "Новый автодром" )
        };
        public static ConsumedEventType GetByCode(ConsumedEventTypesEnum code)
        {
            return Values.Single(v => v.Code == code.ToString());
        }
    }



    public class ConsumedEventState : DomainDictionaryEntry
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
    public class ConsumedEventStates
    {
        public static List<ConsumedEventState> Values = new()
        {
            new ConsumedEventState(1, ConsumedEventStatesEnum.Recieved.ToString(), "Событие получено"),
            new ConsumedEventState(2, ConsumedEventStatesEnum.Processed.ToString(), "Событие обработано"),
            new ConsumedEventState(3, ConsumedEventStatesEnum.ToRepeatProcess.ToString(), "Событие должно быть обработано повторно"),
            new ConsumedEventState(4, ConsumedEventStatesEnum.Processing.ToString(), "Событие обрабатывается"),
            new ConsumedEventState(5, ConsumedEventStatesEnum.Error.ToString(), "При обработке события произошла ошибка")
        };
        public static ConsumedEventState GetByCode(ConsumedEventStatesEnum code)
        {
            return Values.Single(v => v.Code == code.ToString());
        }
    }
}
