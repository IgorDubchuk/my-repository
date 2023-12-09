using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DomainEntities
{
    public class Track
    {
        public Track(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; private set; }
        public string Name { get;
            //private set; }
            set; }

    [ForeignKey("RaceId")]
        public List<Race> Races { get; private set; } = new();
    }
}
