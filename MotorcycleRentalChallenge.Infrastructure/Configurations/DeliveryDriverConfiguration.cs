using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRentalChallenge.Core.Entities;

namespace MotorcycleRentalChallenge.Infrastructure.Configurations
{
    public class DeliveryDriverConfiguration : IEntityTypeConfiguration<DeliveryDriver>
    {
        public void Configure(EntityTypeBuilder<DeliveryDriver> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Identifier).IsRequired().HasMaxLength(20);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Cnpj).IsRequired().HasMaxLength(14);
            builder.HasIndex(x => x.Cnpj).IsUnique();
            builder.Property(x => x.CnhNumber).IsRequired().HasMaxLength(15);
            builder.HasIndex(x => x.CnhNumber).IsUnique();
            builder.Property(x => x.CnhType).IsRequired();
            builder.Property(x => x.Birthdate).IsRequired();
            builder.Property(x => x.CnhImagePath).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
