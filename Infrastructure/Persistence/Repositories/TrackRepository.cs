using Microsoft.EntityFrameworkCore;
using Domain.DomainEntities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Persistence.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly F1DbContext _dbContext;

        public TrackRepository(F1DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Track> GetByNames(IEnumerable<string> names)
        {
            var result = _dbContext.Track.Where(t => names.Contains(t.Name)).ToList();
            return result;
        }
    }
}