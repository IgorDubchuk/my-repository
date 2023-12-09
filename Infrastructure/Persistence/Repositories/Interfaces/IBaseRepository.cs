namespace Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        void Add<D>(D entityToAdd);
        void SaveChanges();
    }
}