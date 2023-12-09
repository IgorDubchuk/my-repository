using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DomainEntities
{
    public class DriverTeamContract
    {
            #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DriverTeamContract() { }
            #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Key]
        public int Id { get; private set; }
        public int DriverId { get; private set; }
        public int TeamId { get; private set; }
        public DateTime DateFrom { get; private set; }
        public DateTime DateTo { get; private set; }


        [ForeignKey("DriverId")]
        public Driver Driver { get; private set; }


        [ForeignKey("TeamId")]
        public Team Team { get; private set; }
    }
}
