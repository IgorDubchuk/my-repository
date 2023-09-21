using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebAPITest.Domain.Models.DomainEvents;

namespace WebAPITest.Infrastructure.Persistence.Configuratoin.ConsumedEvents
{
    public class ConsumedEventTypeConfiguration : IEntityTypeConfiguration<ConsumedEventType>
    {
        public void Configure(EntityTypeBuilder<ConsumedEventType> builder)
        {
            builder.HasData(ConsumedEventTypes.Values);
        }
    }
}
