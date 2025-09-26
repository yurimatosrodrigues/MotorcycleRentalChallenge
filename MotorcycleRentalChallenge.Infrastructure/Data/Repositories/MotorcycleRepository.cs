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

        public async Task<Motorcycle> GetByPlateAsync(string plate)
        {
            return await _context.Set<Motorcycle>()
                .FirstOrDefaultAsync(x => x.Plate == plate.ToUpperInvariant());
        }
    }
}
