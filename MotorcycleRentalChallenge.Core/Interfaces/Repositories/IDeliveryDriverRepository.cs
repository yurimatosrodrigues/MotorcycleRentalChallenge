using MotorcycleRentalChallenge.Core.Entities;

namespace MotorcycleRentalChallenge.Core.Interfaces.Repositories
{
    public interface IDeliveryDriverRepository : IBaseRepository<DeliveryDriver>
    {
        Task<DeliveryDriver> GetByCnpjAsync(string cnpj);
        Task<DeliveryDriver> GetByCnhNumberAsync(string cnhNumber);
    }
}
