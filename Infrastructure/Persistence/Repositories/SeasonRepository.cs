using Domain.DomainEntities;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Persistence.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        private F1DbContext _dbContext;

        public SeasonRepository(F1DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public void SaveNewSeason(Season season)
        //{
        //    _dbContext.Add(season);
        //    //_dbContext.SaveChanges();
        //}

        public List<Season> GetSeasonsByYear(short year)
        {
            var result = _dbContext.Season.Where(t => t.Year == year).ToList();
            return result;
        }
    }
}