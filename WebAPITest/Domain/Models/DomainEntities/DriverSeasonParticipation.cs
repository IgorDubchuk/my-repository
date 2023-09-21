using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITest.Domain.Models.DomainEntities
{
    public class DriverSeasonParticipation
    {
        [Key]
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int SeasonId { get; set; }
        public byte? Position { get; set; }
        public byte? Score { get; set; }


        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }


        [ForeignKey("SeasonId")]
        public Season Season { get; set; }
    }
}
