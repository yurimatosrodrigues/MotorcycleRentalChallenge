using MotorcycleRentalChallenge.Application.InputModel;
using MotorcycleRentalChallenge.Application.Interfaces;
using MotorcycleRentalChallenge.Application.ViewModel;
using MotorcycleRentalChallenge.Core.Exceptions;
using MotorcycleRentalChallenge.Core.Repositories;

namespace MotorcycleRentalChallenge.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        public MotorcycleService(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }
        public async Task AddAsync(AddMotorcycleInputModel model)
        {
            var motorcycle = model.ToEntity();

            var motorcyclePlate = await _motorcycleRepository.GetByPlateAsync(motorcycle.Plate);
            if (motorcyclePlate != null)
            {
                throw new DomainException("There is already a motorcycle with this plate.");
            }

            await _motorcycleRepository.AddAsync(motorcycle);

            return;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MotorcycleViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MotorcycleViewModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, UpdateMotorcycleInputModel model)
        {
            throw new NotImplementedException();
        }
    }
}
