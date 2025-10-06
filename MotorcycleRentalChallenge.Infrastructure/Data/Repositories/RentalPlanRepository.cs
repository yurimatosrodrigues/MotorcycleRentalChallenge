using Microsoft.EntityFrameworkCore;
using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;

namespace MotorcycleRentalChallenge.Infrastructure.Data.Repositories
{
    public class RentalPlanRepository : BaseRepository<RentalPlan>, IRentalPlanRepository
    {
        public RentalPlanRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddRangeAsync(List<RentalPlan> plans)
        {
            await _context.AddRangeAsync(plans);
            await _context.SaveChangesAsync();
        }

        public async Task<RentalPlan> GetByRentalDays(int days)
        {
            return await _context.Set<RentalPlan>().FirstOrDefaultAsync(x => x.Days == days);
        }
    }
}
