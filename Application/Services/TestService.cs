using Domain.DomainEntities;
using Domain.DomainEvents;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services
{
    public class TestService
    {
        private readonly IDictionaryCacheService _dictionaryCacheService;
        private readonly F1DbContext _dbContext;
        private readonly IBaseRepository _baseRepository;
        private readonly ITrackRepository _trackRepository;
        public TestService(IDictionaryCacheService dictionaryCacheService, F1DbContext f1DbContext, IBaseRepository baseRepository, ITrackRepository trackRepository)
        {
            a1 = "l;kjsa;os aspfj asoi[dfj aspoif as0djfas;dkgj asasp ";
            b1 = null;
            _dictionaryCacheService = dictionaryCacheService;
            _dbContext = f1DbContext;
            _baseRepository = baseRepository;
            _trackRepository = trackRepository;
        }

        private string a1 { get; set; }
        private string? b1 { get; set; }
        public void Bench()
        {
            int repeations = 40000000;
            var rand = new Random();

            var beginTime = DateTime.Now;
            for (int i = 1; i<= repeations; i++)
            {
                a1 = rand.NextDouble().ToString();
                a1 = b1 ?? a1;
            }            
            var endTime = DateTime.Now;
            var duration1 = endTime - beginTime;

            beginTime = DateTime.Now;
            for (int i = 1; i <= repeations; i++)
            {
                a1 = rand.NextDouble().ToString();
            }
            endTime = DateTime.Now;
            var duration2 = endTime - beginTime;
            var duration = duration1 - duration2;
        }

        public void Test()
        {
            var nt = new Track("sdgfsdg");
            _baseRepository.Add(nt);
            var tr = _dbContext.Track.ToList();
        }

        public class TestClass
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
        }

        public class TestClassEqualityComparer : IEqualityComparer<TestClass>
        {
            public bool Equals(TestClass? x, TestClass? y)
            {
                if (x != null && y != null)
                    return
                        x!.Id == y!.Id
                        &&
                        x!.Name == y!.Name;
                return false;
            }

            public int GetHashCode(TestClass obj)
            {
                return (obj.Id.ToString()+obj.Name).GetHashCode();
            }
        }
    }
}
