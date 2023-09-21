using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITest.Domain.Models.DomainEntities
{
    public class TeamSeasonParticipation
    {
        [Key]
        public int Id { get; set; }
        public int TeamId { get; set; }
        public byte? Position { get; set; }
        public byte? Score { get; set; }




        [ForeignKey("DriverId")]
        public Team Team { get; set; }
    }
}
