using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebAPITest.Domain.Models.DomainEvents;

namespace WebAPITest.Infrastructure.Persistence.Configuratoin.PublishedEvents
{
    public class PublishedEventTypeConfiguration : IEntityTypeConfiguration<PublishedEventType>
    {
        public void Configure(EntityTypeBuilder<PublishedEventType> builder)
        {
            builder.HasData(PublishedEventTypes.Values);
        }
    }
}
