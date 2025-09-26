using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRentalChallenge.Core.Entities;

namespace MotorcycleRentalChallenge.Infrastructure.Configurations
{
    public class RentalPlanConfiguration : IEntityTypeConfiguration<RentalPlan>
    {
        public void Configure(EntityTypeBuilder<RentalPlan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Days).IsRequired();
            builder.Property(x => x.DailyRate).IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.PenaltyPercentageForUnusedDays).IsRequired(false)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
