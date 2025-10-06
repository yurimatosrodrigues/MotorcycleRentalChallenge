using MotorcycleRentalChallenge.Application.InputModel;
using MotorcycleRentalChallenge.Application.ViewModel;

namespace MotorcycleRentalChallenge.Application.Interfaces
{
    public interface IMotorcycleService
    {
        Task<Guid> AddAsync(AddMotorcycleInputModel model);
        Task<MotorcycleViewModel> GetByIdAsync(Guid id);
        Task<IEnumerable<MotorcycleViewModel>> GetByPlateAsync(string? plate);
        Task UpdateAsync(Guid id, UpdateMotorcycleInputModel model);
        Task DeleteAsync(Guid id);        
    }
}
