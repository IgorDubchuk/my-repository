using WebAPITest.Domain.Models.DomainEntities;

namespace WebAPITest.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface ISeasonRepository
    {
        List<Season> GetSeasonsByYear(short year);
        void SaveNewSeason(Season season);
    }
}