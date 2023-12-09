using System.ComponentModel.DataAnnotations;

namespace Domain.DomainEntities
{
    public class Team
    {
            #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Team() { }
            #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }


        public List<DriverTeamContract> DriverTeamContracts { get; private set; } = new();
        public List<TeamRaceParticipation> TeamRaceParticipations { get; private set; } = new();
        public List<TeamSeasonParticipation> TeamSeasonParticipations { get; private set; } = new();
    }
}
