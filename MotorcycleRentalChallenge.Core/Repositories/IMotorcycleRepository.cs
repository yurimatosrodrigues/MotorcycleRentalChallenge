using MotorcycleRentalChallenge.Core.Entities;

namespace MotorcycleRentalChallenge.Core.Repositories
{
    public interface IMotorcycleRepository : IBaseRepository<Motorcycle>
    {
        Task<Motorcycle> GetByPlateAsync(string plate);
    }
}
