using Microsoft.EntityFrameworkCore;
using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Repositories;

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
    }
}
