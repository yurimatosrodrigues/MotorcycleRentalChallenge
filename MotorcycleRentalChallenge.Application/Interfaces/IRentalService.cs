using MotorcycleRentalChallenge.Application.InputModel;
using MotorcycleRentalChallenge.Application.ViewModel;

namespace MotorcycleRentalChallenge.Application.Interfaces
{
    public interface IRentalService
    {
        Task<Guid> AddAsync(AddRentalInputModel model);
        Task<RentalViewModel> GetByIdAsync(Guid id);
        Task<decimal> UpdateAsync(Guid id, UpdateRentalInputModel model);
    }
}
