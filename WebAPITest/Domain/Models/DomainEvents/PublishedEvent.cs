namespace WebAPITest.Domain.Models.DomainEvents
{
    public class PublishedEvent
    {
        private PublishedEvent()
        {

        }
        public PublishedEvent(PublishedEventType type, string data)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            CreateDateTime = DateTime.Now;
            Data = data ?? throw new ArgumentNullException(nameof(Data));
        }

        public int Id { get; private set; }
        public PublishedEventType Type { get; private set; }
        public DateTime CreateDateTime { get; private set; }
        public string Data { get; private set; }
    }

    public class PublishedEventType
    {
        public PublishedEventType(int id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }


        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
    }

    public static class PublishedEventTypes
    {
        public static List<PublishedEventType> Values = new()
        {
            new PublishedEventType(1, "AfterRaceDriverStandings", "Позиции в чемпионате мира по итогам гонки" ),
            new PublishedEventType (2, "AfterRaceCunstructorStandings", "Позиции в кубке конструкторов по итогам гонки"),
            new PublishedEventType (3, "DriverChampionDetermined", "Определен чемпион мира" ),
            new PublishedEventType (4, "ConstructorChampionDetermined", "Определен обладатель кубка конструкторов" )
        };
    }
}