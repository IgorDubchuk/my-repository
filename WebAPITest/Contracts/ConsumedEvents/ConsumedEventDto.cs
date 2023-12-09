using FluentValidation;

namespace API.Contracts.ConsumedEvents
{
    public record ConsumedEventDto(DateTime EventDateTime);


    public class ConsumedEventDtoValidator : AbstractValidator<ConsumedEventDto>
    {
        public ConsumedEventDtoValidator()
        {
            RuleFor(dto => dto.EventDateTime).NotEmpty();
        }
    }
}