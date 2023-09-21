using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITest.Domain.Models.DomainEntities
{
    public class DriverRaceParticipation
    {
        [Key]
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int TeamId { get; set; }
        public int RaceId { get; set; }
        public byte? Position { get; set; }
        public byte? ScoreForRace { get; set; }
        public byte? ScoreInSeasonAfterRace { get; set; }





        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }

        [ForeignKey("RaceId")]
        public Race Race { get; set; }
    }
}
