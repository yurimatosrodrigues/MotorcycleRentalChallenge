using Microsoft.EntityFrameworkCore;
using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Infrastructure.Configurations;

namespace MotorcycleRentalChallenge.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options) { }

        DbSet<Motorcycle> Motorcycles { get; set; }
        DbSet<DeliveryDriver> DeliveryDrivers { get; set; }
        DbSet<Rental> Rentals {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MotorcycleConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryDriverConfiguration());
            modelBuilder.ApplyConfiguration(new RentalConfiguration());
            modelBuilder.ApplyConfiguration(new RentalPlanConfiguration());

            modelBuilder.Entity<RentalPlan>().HasData(
                new RentalPlan(7, 30m, 0.2m),
                new RentalPlan(15, 28m, 0.4m),
                new RentalPlan(30, 22m, null),
                new RentalPlan(45, 20m, null),
                new RentalPlan(50, 18m, null)
            );
        }
    }
}
