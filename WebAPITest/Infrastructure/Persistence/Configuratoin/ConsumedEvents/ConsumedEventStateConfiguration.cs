using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebAPITest.Domain.Models.DomainEvents;

namespace WebAPITest.Infrastructure.Persistence.Configuratoin.ConsumedEvents
{
    public class ConsumedEventStateConfiguration : IEntityTypeConfiguration<ConsumedEventState>
    {
        public void Configure(EntityTypeBuilder<ConsumedEventState> builder)
        {
            builder.HasData(ConsumedEventStates.Values);
        }
    }
}
