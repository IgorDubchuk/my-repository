using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITest.Domain.Models.DomainEntities
{
    public class Race
    {
        private Race()
        {

        }

        //хотелось бы, чтобы гонка могла быть создана только в контексте создания сезона, или в последствии метода сезона "добавить гонку"
        //как это можно сделать?
        public Race(byte numberInSeason, string name, DateTime date, Track track)
        {
            if (numberInSeason <= 0) throw new ApplicationException("Can not create race: race number should be bigger than zero.");

            NumberInSeason = numberInSeason;
            Name = name;
            Date = date;
            Track = track;
        }


        [Key]
        public int Id { get; set; }
        public byte NumberInSeason { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }



        [ForeignKey("SeasonId")]
        public Season Season { get; set; }
        public Track Track { get; set; }
        public List<DriverRaceParticipation> DriverRaceParticipations { get; set; } = new();
        public List<TeamRaceParticipation> TeamRaceParticipations { get; set; } = new();
    }
}
