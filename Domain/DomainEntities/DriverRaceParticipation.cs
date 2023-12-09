using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DomainEntities
{
    public class DriverRaceParticipation
    {
            #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private DriverRaceParticipation()
            #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            
        }

        [Key]
        public int Id { get; private set; }
        public int DriverId { get; private set; }
        public int TeamId { get; private set; }
        public int RaceId { get; private set; }
        public byte? Position { get; private set; }
        public byte? ScoreForRace { get; private set; }
        public byte? ScoreInSeasonAfterRace { get; private set; }


        [ForeignKey("DriverId")]
        public Driver Driver { get; private set; }

        [ForeignKey("TeamId")]
        public Team Team { get; private set; }

        [ForeignKey("RaceId")]
        public Race Race { get; private set; }
    }
}
