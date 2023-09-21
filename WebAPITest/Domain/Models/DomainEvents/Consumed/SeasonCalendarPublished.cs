using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPITest.Contracts;
using WebAPITest.Domain.Models.DomainEvents;

namespace WebAPITest.Domain.Models.DomainEvents.Consumed
{
    public class SeasonCalendarPublished : ConsumedEvent
    {
        private SeasonCalendarPublished()
        {

        }
        public SeasonCalendarPublished(DateTime eventDateTime, short year, IEnumerable<SeasonCalendarRace> races) : base(ConsumedEventTypes.GetByCode(ConsumedEventTypesEnum.SeasonCalendarPublished), eventDateTime)
        {
            if (races.IsNullOrEmpty()) { throw new ApplicationException("Can not create domain event SeasonCalendarPublished: no races included."); }
            if (races.DistinctBy(r => r.NumberInSeason).Count() < races.Count()) { throw new ApplicationException("Can not create domain event SeasonCalendarPublished: each race should have unique number in season."); }
            if (races.MaxBy(r => r.NumberInSeason).NumberInSeason != races.Count()) { throw new ApplicationException("Can not create domain event SeasonCalendarPublished: race number should be consecutive, and begin from 1."); }
            if (races.Any(r => r.Date.Year != year)) { throw new ApplicationException("Can not create domain event SeasonCalendarPublished: each race should have date in season year."); }

            //Races should be sorted by number and be consequative by date
            SortedList<byte, SeasonCalendarRace> sortedByNumberRaces = new SortedList<byte, SeasonCalendarRace>(races.ToDictionary(r => r.NumberInSeason, r => r));
            DateTime previouseRaceDate = DateTime.MinValue;
            foreach (var race in sortedByNumberRaces.Values)
            {
                if (race.Date <= previouseRaceDate) { throw new ApplicationException("Can not create domain event SeasonCalendarPublished: races should be consequative by date."); }
                previouseRaceDate = race.Date;
            }

            Year = year;
            Races = sortedByNumberRaces.Select(d => d.Value).ToList();
        }

        public short Year { get; set; }
        public IEnumerable<SeasonCalendarRace> Races { get; set; } = Enumerable.Empty<SeasonCalendarRace>();

        //public SeasonCalendarRace CreateAndSetRaceToCalendar(
        //        string trackName,
        //        string name,
        //        DateTime date)
        //{
        //    var race = new SeasonCalendarRace(trackName, (byte)(this.Races.Count()+1), name, date);
        //    Races = Races.Append(race);
        //    return race;
        //}

        public class SeasonCalendarRace
        {
            //как сделать так, чтобы этот конструктор был доступен только классу выше?
            public SeasonCalendarRace(
                string trackName,
                byte numberInSeason,
                string name,
                DateTime date)
            {
                TrackName = trackName;
                NumberInSeason = numberInSeason;
                Name = name;
                Date = date;
            }

            public string TrackName { get; private set; }
            public byte NumberInSeason { get; private set; }
            public string Name { get; private set; }
            public DateTime Date { get; private set; }
        }
    }
}