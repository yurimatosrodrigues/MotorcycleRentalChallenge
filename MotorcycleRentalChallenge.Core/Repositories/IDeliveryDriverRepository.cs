using MotorcycleRentalChallenge.Core.Entities;

namespace MotorcycleRentalChallenge.Core.Repositories
{
    public interface IDeliveryDriverRepository : IBaseRepository<DeliveryDriver>
    {
        Task<DeliveryDriver> GetByCnpjAsync(string cnpj);
        Task<DeliveryDriver> GetByCnhNumberAsync(string cnhNumber);
    }
}
