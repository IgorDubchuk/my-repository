namespace WebAPITest.Contracts.ConsumedEvents
{
    public class SeasonCalendarPublishedDto : ConsumedEventDto
    {
        public short Year { get; set; }
        public IEnumerable<SeasonCalendarRaceDto> Races { get; set; } = Enumerable.Empty<SeasonCalendarRaceDto>();
    }
    public class SeasonCalendarRaceDto
    {
        public string TrackName { get; set; }
        public byte NumberInSeason { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
