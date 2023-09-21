using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITest.Domain.Models.DomainEntities
{
    public class Track
    {
        private Track()
        {

        }
        public Track(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


        [ForeignKey("RaceId")]
        public List<Race> Races { get; set; } = new();
    }
}
