using Microsoft.EntityFrameworkCore;
using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;

namespace MotorcycleRentalChallenge.Infrastructure.Data.Repositories
{
    public class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
        public RentalRepository(AppDbContext context) : base(context)
        {            
        }
                
        public override async Task<Rental> GetByIdAsync(Guid id)
        {
            return await _context.Set<Rental>()
                .Include(r => r.RentalPlan)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
