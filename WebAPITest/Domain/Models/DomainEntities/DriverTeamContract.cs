using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITest.Domain.Models.DomainEntities
{
    public class DriverTeamContract
    {
        [Key]
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int TeamId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }


        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }


        [ForeignKey("TeamId")]
        public Team Team { get; set; }
    }
}
