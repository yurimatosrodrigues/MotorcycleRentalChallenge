using Microsoft.EntityFrameworkCore;
using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;

namespace MotorcycleRentalChallenge.Infrastructure.Data.Repositories
{
    public class MotorcycleRepository : BaseRepository<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Motorcycle>> GetByPlateAsync(string? plate)
        {
            var query = _context.Set<Motorcycle>().AsQueryable();
            if (!string.IsNullOrWhiteSpace(plate))
            {
                query = query.Where(x => x.Plate == plate.ToUpperInvariant());
            }

            return await query.ToListAsync();
        }

        public async Task<Motorcycle> GetByIdAsync(Guid id)
        {
            return await _context.Set<Motorcycle>()
                .Include(r => r.Rentals)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
