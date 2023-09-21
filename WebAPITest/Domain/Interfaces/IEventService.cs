using WebAPITest.Domain.Models.DomainEvents.Consumed;

namespace WebAPITest.Domain.Interfaces
{
    public interface IEventService
    {
        void ProcessEvent(NewTrack newTrackEvent);
        void ProcessEvent(SeasonCalendarPublished newTrackEvent);
    }
}