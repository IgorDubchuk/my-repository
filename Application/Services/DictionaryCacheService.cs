using System.Reflection;
using Domain.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Infrastructure.Persistence;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services
{
    public interface IDictionaryCacheService
    {
        List<D> GetDictionary<D>() where D : class;
    }

    public class DictionaryCacheService : IDictionaryCacheService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<EventSourcingSingletonService> _logger;
        private readonly IServiceScope _serviceScope;
        private readonly F1DbContext _dbContext;

        private readonly List<Type> cachedDictionaries = new()
        {
            typeof(ConsumedEventState),
            typeof(ConsumedEventType),
            typeof(PublishedEventType)
        };
        private readonly Dictionary<string, List<Object>> dictionaries = new Dictionary<string, List<object>>();

        public DictionaryCacheService(IServiceScopeFactory serviceScopeFactory, ILogger<EventSourcingSingletonService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

            _logger.LogInformation("Loading dictionary cache...");

            _serviceScope = _serviceScopeFactory.CreateScope();
            _dbContext = _serviceScope.ServiceProvider.GetRequiredService<F1DbContext>();

            MethodInfo? LoadDictionaryMethod = typeof(DictionaryCacheService).GetMethod("LoadDictionary");
            if (LoadDictionaryMethod != null)
            {
                foreach (Type type in cachedDictionaries)
                {
                    LoadDictionaryMethod.MakeGenericMethod(type).Invoke(this, null); // No target, no arguments
                }
            }
            else throw new ApplicationException("Method for dictionary loading was not found in DictionaryCacheService class");

            _logger.LogInformation("Dictionary cache loaded");
        }

        //Used in constructor indirectly
        public void LoadDictionary<D>() where D : class
        {
            var dictionaryEntries = _dbContext.Set<D>().ToList();
            dictionaries.Add(typeof(D).Name, new List<object>(dictionaryEntries));
        }

        public List<D> GetDictionary<D>() where D : class
        {
            string key = typeof(D).Name;
            var dictionaryAsObjects = dictionaries[key];
            var result = new List<D>();
            foreach (var item in dictionaryAsObjects)
            {
                if (item as D == null)
                    throw new ApplicationException($"Can not obtain dictionary from dictionary cache." +
                        $"For key {typeof(D).Name} in cache there is entry of another type");
                result.Add((item as D)!);
            }
            return result;
        }
    }
}
