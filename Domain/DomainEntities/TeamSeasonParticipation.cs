using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DomainEntities
{
    public class TeamSeasonParticipation
    {
            #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public TeamSeasonParticipation() { }
            #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.



        [Key]
        public int Id { get; private set; }
        public int TeamId { get; private set; }
        public byte? Position { get; private set; }
        public byte? Score { get; private set; }




        [ForeignKey("DriverId")]
        public Team Team { get; private set; }
    }
}
