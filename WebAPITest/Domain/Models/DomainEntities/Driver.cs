using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITest.Domain.Models.DomainEntities
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }


        public List<DriverTeamContract> DriverTeamContracts { get; set; } = new();
        public List<DriverSeasonParticipation> DriverSeasonParticipations { get; set; } = new();
        public List<DriverRaceParticipation> DriverRaceParticipations { get; set; } = new();
    }
}
