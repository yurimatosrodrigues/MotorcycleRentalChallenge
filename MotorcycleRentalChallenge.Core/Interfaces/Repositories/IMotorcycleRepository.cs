using MotorcycleRentalChallenge.Core.Entities;

namespace MotorcycleRentalChallenge.Core.Interfaces.Repositories
{
    public interface IMotorcycleRepository : IBaseRepository<Motorcycle>
    {
        Task<IEnumerable<Motorcycle>> GetByPlateAsync(string? plate);
    }
}
