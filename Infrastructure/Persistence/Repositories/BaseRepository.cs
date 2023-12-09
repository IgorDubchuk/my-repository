using System.Reflection;
using Domain;
using Infrastructure.Persistence.Repositories.Interfaces;

namespace Infrastructure.Persistence.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly Guid InstanceId;
        private readonly F1DbContext _dbContext;
        private readonly IEnumerable<Type> domainDictionaries;
        private readonly MethodInfo? SetAllEntriesAsUnchangedMethod;

        public BaseRepository(F1DbContext dbContext)
        {
            InstanceId = Guid.NewGuid();
            _dbContext = dbContext;

            domainDictionaries = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                .Where(mytype => mytype.IsSubclassOf(typeof(DomainDictionaryEntry)));

            SetAllEntriesAsUnchangedMethod = typeof(BaseRepository).GetMethod("SetAllEntriesAsUnchanged");
        }

        public void SaveChanges()
        {
            foreach (var type in domainDictionaries)
            {
                var a = SetAllEntriesAsUnchangedMethod!.MakeGenericMethod(type);
                a.Invoke(this, null); // No target, no arguments
            }
            _dbContext.SaveChanges();
        }

        public void Add<D>(D entityToAdd)
        {
            _dbContext.Add(entityToAdd!);
        }

        //Used in constructor indirectly
        public void SetAllEntriesAsUnchanged<D>() where D : class
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries<D>())
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
        }
    }
}