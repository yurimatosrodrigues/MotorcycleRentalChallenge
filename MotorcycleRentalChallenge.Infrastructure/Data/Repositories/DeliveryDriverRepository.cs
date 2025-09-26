using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Repositories;

namespace MotorcycleRentalChallenge.Infrastructure.Data.Repositories
{
    public class DeliveryDriverRepository : BaseRepository<DeliveryDriver>, IDeliveryDriverRepository
    {
        public Task<DeliveryDriver> GetByCnhNumberAsync(string cnhNumber)
        {
            throw new NotImplementedException();
        }

        public Task<DeliveryDriver> GetByCnpjAsync(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
