using Domain.DomainEntities;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface ITrackRepository
    {
        public List<Track> GetByNames(IEnumerable<string> name);
    }
}