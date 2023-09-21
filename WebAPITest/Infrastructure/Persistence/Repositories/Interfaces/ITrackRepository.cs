using WebAPITest.Domain.Models.DomainEntities;

namespace WebAPITest.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface ITrackRepository
    {
        public List<Track> GetByNames(IEnumerable<string> name);
        public void SaveNewTrack(Track track);
    }
}