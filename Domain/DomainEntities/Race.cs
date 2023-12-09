using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DomainEntities
{
    public class Race
    {
            #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Race() { }
            #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
        public int Id { get; private set; }
        public byte NumberInSeason { get; private set; }
        public string Name { get; private set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; private set; }



        [ForeignKey("SeasonId")]
        //Вопрос: сезон без гонок не имеет смысла, гонки вне сезона тоже. Как сделать так, чтобы
        //на уровне модели и то, и то было обязательным, но при этом не было ворнинга на тему того
        //что в конструкторе не задается обязательное поле? Мы ведь все равно сначала создаем одно, а потом второе...
        public Season? Season { get; private set; }
        public Track Track { get; private set; }
        public List<DriverRaceParticipation> DriverRaceParticipations { get; private set; } = new();
        public List<TeamRaceParticipation> TeamRaceParticipations { get; private set; } = new();
    }
}
