using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using static WebAPITest.Domain.Models.DomainEvents.Consumed.SeasonCalendarPublished;

namespace WebAPITest.Domain.Models.DomainEntities
{
    public class Season
    {
        private Season()
        {

        }

        public Season(short year, List<Race> races)
        {
            if (races.IsNullOrEmpty()) { throw new ApplicationException("Can not create new season with no races."); }
            if (races.DistinctBy(r => r.NumberInSeason).Count() < races.Count()) { throw new ApplicationException("Can not create season: each race should have unique number in season."); }
            if (races.MaxBy(r => r.NumberInSeason).NumberInSeason != races.Count()) { throw new ApplicationException("Can not create season: race number should be consecutive, and begin from 1."); }
            if (races.Any(r => r.Date.Year != year)) { throw new ApplicationException("Can not create season: each race should have date in season year."); }

            //Races should be sorted by number and be consequative by date
            SortedList<byte, Race> sortedByNumberRaces = new SortedList<byte, Race>(races.ToDictionary(r => r.NumberInSeason, r => r));
            DateTime previouseRaceDate = DateTime.MinValue;
            foreach (var race in sortedByNumberRaces.Values)
            {
                if (race.Date <= previouseRaceDate) { throw new ApplicationException("Can not create season: races should be consequative by date."); }
                previouseRaceDate = race.Date;
            }

            Year = year;
            Races = sortedByNumberRaces.Select(d => d.Value).ToList();
            //Не понятно, нужно ли инварианты дублировать в соответствующее событие?

            Year = year;
            Races = races;
        }

        [Key]
        public int Id { get; set; }
        public short Year { get; set; }

        public List<Race> Races { get; set; }
        public List<DriverSeasonParticipation> DriverSeasonParticipations { get; set; } = new();
        public List<TeamSeasonParticipation> TeamSeasonParticipations { get; set; } = new();
    }
}
