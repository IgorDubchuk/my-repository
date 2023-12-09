using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DomainEntities
{
    public class Driver
    {
        private Driver(int id, string name, DateTime dateOfBirth)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public Driver(string name, DateTime dateOfBirth, List<DriverTeamContract> driverTeamContracts, List<DriverSeasonParticipation> driverSeasonParticipations, List<DriverRaceParticipation> driverRaceParticipations)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            DriverTeamContracts = driverTeamContracts;
            DriverSeasonParticipations = driverSeasonParticipations;
            DriverRaceParticipations = driverRaceParticipations;
        }

        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; private set; }


        public List<DriverTeamContract> DriverTeamContracts { get; private set; } = new();
        public List<DriverSeasonParticipation> DriverSeasonParticipations { get; private set; } = new();
        public List<DriverRaceParticipation> DriverRaceParticipations { get; private set; } = new();
    }
}
