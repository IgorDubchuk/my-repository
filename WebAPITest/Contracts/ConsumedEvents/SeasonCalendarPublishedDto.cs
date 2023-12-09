using FluentValidation;

namespace API.Contracts.ConsumedEvents
{
    public record SeasonCalendarPublishedDto(
        DateTime EventDateTime,
        short Year,
        IEnumerable<SeasonCalendarRaceDto> Races) : ConsumedEventDto(EventDateTime);

    public record SeasonCalendarRaceDto(
        string TrackName,
        byte NumberInSeason,
        string Name,
        DateTime Date);


    public class SeasonCalendarPublishedDtoValidator : AbstractValidator<SeasonCalendarPublishedDto>
    {
        public SeasonCalendarPublishedDtoValidator()
        {
            RuleFor(dto => dto.EventDateTime).NotEmpty();
            RuleFor(dto => dto.Year).NotEmpty();
            RuleFor(dto => dto.Races).NotEmpty();
        }
    }
}
