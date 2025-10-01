using Microsoft.EntityFrameworkCore;
using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;

namespace MotorcycleRentalChallenge.Infrastructure.Data.Repositories
{
    public class DeliveryDriverRepository : BaseRepository<DeliveryDriver>, IDeliveryDriverRepository
    {
        public DeliveryDriverRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<DeliveryDriver> GetByCnhNumberAsync(string cnhNumber)
        {
            return await _context.Set<DeliveryDriver>()
                .FirstOrDefaultAsync(x => x.CnhNumber == cnhNumber);
        }

        public async Task<DeliveryDriver> GetByCnpjAsync(string cnpj)
        {
            return await _context.Set<DeliveryDriver>()
                .FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }
    }
}
