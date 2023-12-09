using Application.Interfaces;
using Domain.DomainEntities;
using Domain.DomainEvents;
using Domain.DomainEvents.Consumed;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly Guid InstanceId;
        private readonly IBaseRepository _baseRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly ISeasonRepository _seasonRepository;
        public EventService(IBaseRepository baseRepository, ITrackRepository trackRepository, ISeasonRepository seasonRepository)
        {
            InstanceId = Guid.NewGuid();
            _baseRepository = baseRepository;
            _trackRepository = trackRepository;
            _seasonRepository = seasonRepository;
        }

        public void ProcessEvent(NewTrack eventToProcess, bool reProcess)
        {
            //No action if re-processing this event type 
            if (reProcess)
                return;

            var tracksWithSameName = _trackRepository.GetByNames(new List<string>() { eventToProcess.Name });
            if (tracksWithSameName.Count != 0)
                throw new ApplicationException($"Error while processing NewTrack event: track with name {eventToProcess.Name} allready exists.");

            var track = new Track(eventToProcess.Name);
            _baseRepository.Add(track);
        }

        public void ProcessEvent(SeasonCalendarPublished eventToProcess, bool reProcess)
        {
            //No action if re-processing this event type 
            if (reProcess)
                return;

            //Checking for existing seasons
            var seasonsForYear = _seasonRepository.GetSeasonsByYear(eventToProcess.Year);
            if (seasonsForYear.Count == 1)
            {
                throw new ApplicationException($"Error while processing SeasonCalendarPublished event: season for year {eventToProcess.Year} allready exists.");
            }
            if (seasonsForYear.Count > 1)
            {
                throw new ApplicationException($"Error while processing SeasonCalendarPublished event: incorrect data in repository: for year {eventToProcess.Year} more than one season was found.");
            }

            //Checking tracks
            var trackNamesFromEvent = eventToProcess.Races.Select(r => r.TrackName).Distinct().ToList();
            var tracks = _trackRepository.GetByNames(trackNamesFromEvent);
            var distinctTracks = tracks.DistinctBy(t => t.Name).ToList();
            if (distinctTracks.Count != tracks.Count()) throw new ApplicationException("Error while processing SeasonCalendarPublished event: bad data in repository: one ore more tracks have doubles by names.");
            if (distinctTracks.Count != trackNamesFromEvent.Count()) throw new ApplicationException("Error while processing SeasonCalendarPublished event: one or more tracks were not found by name.");

            //Creating domain entities from event data
            List<Race> races = new List<Race>();
            races = eventToProcess.Races.Select(e => new Race(e.NumberInSeason, e.Name, e.Date, tracks.Single(t => t.Name == e.TrackName))).ToList();
            Season season = new(eventToProcess.Year, races);

            _baseRepository.Add(season);
        }
    }
}
