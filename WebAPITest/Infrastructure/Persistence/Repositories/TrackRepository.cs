using Microsoft.EntityFrameworkCore;
using WebAPITest.Domain.Models.DomainEntities;
using WebAPITest.Infrastructure.Persistence;
using WebAPITest.Infrastructure.Persistence.Repositories.Interfaces;

namespace WebAPITest.Infrastructure.Persistence.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly F1DbContext _dbContext;

        public TrackRepository(F1DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void SaveNewTrack(Track track)
        {
            _dbContext.Add(track);
            _dbContext.SaveChanges();
        }


        public List<Track> GetByNames(IEnumerable<string> names)
        {
            var result = _dbContext.Track.Where(t => names.Contains(t.Name)).ToList();
            return result;
        }
    }
}