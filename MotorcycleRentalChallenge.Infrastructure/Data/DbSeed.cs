using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;

namespace MotorcycleRentalChallenge.Infrastructure.Data
{
    public class DbSeed
    {
        private readonly IRentalPlanRepository _rentalPlanRepository;

        public DbSeed(IRentalPlanRepository rentalPlanRepository)
        {
            _rentalPlanRepository = rentalPlanRepository;
        }

        public async Task Populate()
        {
            var data = await _rentalPlanRepository.GetAllAsync();
            if (data == null || !data.Any())
            {
                var plans = new List<RentalPlan>() {
                    new RentalPlan(7, 30m, 0.2m),
                    new RentalPlan(15, 28m, 0.4m),
                    new RentalPlan(30, 22m, null),
                    new RentalPlan(45, 20m, null),
                    new RentalPlan(50, 18m, null)
                };
                await _rentalPlanRepository.AddRangeAsync(plans);
            }
        }
    }
}
