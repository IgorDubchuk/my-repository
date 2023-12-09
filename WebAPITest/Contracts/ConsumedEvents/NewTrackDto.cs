namespace API.Contracts.ConsumedEvents
{
    public record NewTrackDto(DateTime EventDateTime, string Name) : ConsumedEventDto(EventDateTime);
}
