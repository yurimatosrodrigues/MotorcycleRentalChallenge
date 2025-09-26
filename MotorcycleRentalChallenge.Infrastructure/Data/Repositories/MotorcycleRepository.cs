using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Repositories;

namespace MotorcycleRentalChallenge.Infrastructure.Data.Repositories
{
    public class MotorcycleRepository : BaseRepository<Motorcycle>, IMotorcycleRepository
    {
        public Task<Motorcycle> GetByPlateAsync(string plate)
        {
            throw new NotImplementedException();
        }
    }
}
