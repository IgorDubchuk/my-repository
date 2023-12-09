using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.DomainEvents;

namespace Infrastructure.Persistence.Configuratoin.ConsumedEvents
{
    public class ConsumedEventTypeConfiguration : IEntityTypeConfiguration<ConsumedEventType>
    {
        public void Configure(EntityTypeBuilder<ConsumedEventType> builder)
        {
            builder.HasData(ConsumedEvent.Types);
        }
    }
}
