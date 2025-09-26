using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRentalChallenge.Core.Entities;

namespace MotorcycleRentalChallenge.Infrastructure.Configurations
{
    public class MotorcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.HasKey(x => x.Id); 

            builder.Property(x => x.Plate).IsRequired().HasMaxLength(8);
            builder.HasIndex(x => x.Plate).IsUnique();

            builder.Property(x => x.Model).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Year).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
