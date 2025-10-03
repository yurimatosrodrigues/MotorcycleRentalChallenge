using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRentalChallenge.Core.Entities;

namespace MotorcycleRentalChallenge.Infrastructure.Configurations
{
    public class MotorcycleNotificationConfiguration : IEntityTypeConfiguration<MotorcycleNotification>
    {
        public void Configure(EntityTypeBuilder<MotorcycleNotification> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MotorcycleId).IsRequired();
            builder.Property(x => x.Plate).IsRequired();                   
            builder.Property(x => x.Year).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
