using MotorcycleRentalChallenge.Application.InputModel;
using MotorcycleRentalChallenge.Application.ViewModel;

namespace MotorcycleRentalChallenge.Application.Interfaces
{
    public interface IDeliveryDriverService
    {
        Task<Guid> AddAsync(AddDeliveryDriverInputModel model);
        Task SendCnhImageAsync(Guid id, CnhImageInputModel model);        
    }
}
