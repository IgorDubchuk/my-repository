using System.ComponentModel.DataAnnotations;

namespace WebAPITest.Domain.Models.DomainEntities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


        public List<DriverTeamContract> DriverTeamContracts { get; set; } = new();
        public List<TeamRaceParticipation> TeamRaceParticipations { get; set; } = new();
        public List<TeamSeasonParticipation> TeamSeasonParticipations { get; set; } = new();
    }
}
