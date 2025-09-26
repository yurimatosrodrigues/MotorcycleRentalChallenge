using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRentalChallenge.Core.Entities;
using System.Reflection.Emit;

namespace MotorcycleRentalChallenge.Infrastructure.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MotorcycleId).IsRequired();
            builder.Property(x => x.DeliveryDriverId).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired(false);
            builder.Property(x => x.ExpectedEndDate).IsRequired();
            builder.Property(x => x.RentalPlanId).IsRequired();
            builder.Property(x => x.DailyRate).IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.TotalCost).IsRequired()
                .HasColumnType("decimal(18,2)").HasDefaultValue(0);
            builder.Property(x => x.CreatedAt).IsRequired();
                        
            builder.HasOne(x => x.Motorcycle)
                .WithMany(m => m.Rentals)
                .HasForeignKey(x => x.MotorcycleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DeliveryDriver)
                .WithMany(d => d.Rentals)
                .HasForeignKey(x => x.DeliveryDriverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RentalPlan)
                .WithMany(r => r.Rentals)
                .HasForeignKey(x => x.RentalPlanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
