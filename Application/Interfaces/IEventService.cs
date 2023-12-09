using Domain.DomainEvents.Consumed;

namespace Application.Interfaces
{
    public interface IEventService
    {
        void ProcessEvent(NewTrack newTrackEvent, bool reProcess);
        void ProcessEvent(SeasonCalendarPublished newTrackEvent, bool reProcess);
    }
}