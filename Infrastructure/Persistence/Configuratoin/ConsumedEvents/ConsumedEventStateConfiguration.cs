using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.DomainEvents;

namespace Infrastructure.Persistence.Configuratoin.ConsumedEvents
{
    public class ConsumedEventStateConfiguration : IEntityTypeConfiguration<ConsumedEventState>
    {
        public void Configure(EntityTypeBuilder<ConsumedEventState> builder)
        {
            builder.HasData(ConsumedEvent.States);
        }
    }
}
