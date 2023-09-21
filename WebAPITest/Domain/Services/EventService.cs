using WebAPITest.Domain.Interfaces;
using WebAPITest.Domain.Models.DomainEntities;
using WebAPITest.Domain.Models.DomainEvents.Consumed;
using WebAPITest.Infrastructure.Persistence.Repositories.Interfaces;

namespace WebAPITest.Domain.Services
{
    public class EventService : IEventService
    {
        private readonly ITrackRepository _trackRepository;
        private readonly ISeasonRepository _seasonRepository;

        public EventService(ITrackRepository trackRepository, ISeasonRepository seasonRepository)
        {
            _trackRepository = trackRepository;
            _seasonRepository = seasonRepository;
        }


        public void ProcessEvent(NewTrack eventToProcess)
        {
            var track = new Track(eventToProcess.Name);
            _trackRepository.SaveNewTrack(track);
        }


        public void ProcessEvent(SeasonCalendarPublished eventToProcess)
        {
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
            var trackNames = eventToProcess.Races.Select(r => r.TrackName).Distinct().ToList();
            var tracks = _trackRepository.GetByNames(trackNames);
            var distinctTracks = tracks.DistinctBy(t => t.Name).ToList();
            if (distinctTracks.Count != tracks.Count()) throw new ApplicationException("Error while processing SeasonCalendarPublished event: bad data in repository: one ore more tracks have doubles by names.");
            if (distinctTracks.Count != trackNames.Count()) throw new ApplicationException("Error while processing SeasonCalendarPublished event: one or more tracks were not found by name.");

            //Creating domain entities from event data
            List<Race> races = new List<Race>();
            races = eventToProcess.Races.Select(e => new Race(e.NumberInSeason, e.Name, e.Date, tracks.Single(t => t.Name == e.TrackName))).ToList();
            Season season = new(eventToProcess.Year, races);

            _seasonRepository.SaveNewSeason(season);
        }
    }
}
