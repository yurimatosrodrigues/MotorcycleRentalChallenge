using MotorcycleRentalChallenge.Core.Entities;

namespace MotorcycleRentalChallenge.Core.Interfaces.Repositories
{
    public interface IRentalPlanRepository : IBaseRepository<RentalPlan>
    {
        Task<RentalPlan> GetByRentalDays(int days);
        Task AddRangeAsync(List<RentalPlan> plans);
    }
}
