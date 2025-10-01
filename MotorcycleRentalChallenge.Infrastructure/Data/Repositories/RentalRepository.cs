using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;

namespace MotorcycleRentalChallenge.Infrastructure.Data.Repositories
{
    public class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
        public RentalRepository(AppDbContext context) : base(context)
        {
        }
    }
}
