using Domain.DomainEntities;

namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface ISeasonRepository
    {
        List<Season> GetSeasonsByYear(short year);
    }
}