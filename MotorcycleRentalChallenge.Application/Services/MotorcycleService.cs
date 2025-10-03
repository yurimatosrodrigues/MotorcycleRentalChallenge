using MotorcycleRentalChallenge.Application.InputModel;
using MotorcycleRentalChallenge.Application.Interfaces;
using MotorcycleRentalChallenge.Application.ViewModel;
using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Events;
using MotorcycleRentalChallenge.Core.Exceptions;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;
using MotorcycleRentalChallenge.Infrastructure.Messaging;

namespace MotorcycleRentalChallenge.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMessageBusService _rabbitMqService;
        public MotorcycleService(IMotorcycleRepository motorcycleRepository, IMessageBusService rabbitMqService)
        {
            _motorcycleRepository = motorcycleRepository;
            _rabbitMqService = rabbitMqService;
        }

        public async Task<Guid> AddAsync(AddMotorcycleInputModel model)
        {
            var motorcycle = model.ToEntity();

            var motorcyclePlate = await _motorcycleRepository.GetByPlateAsync(motorcycle.Plate);
            if (motorcyclePlate.Any())
            {
                throw new DomainException("There is already a motorcycle with this plate.");
            }

            var id = await _motorcycleRepository.AddAsync(motorcycle);

            await _rabbitMqService.PublishAsync(
                new MotorcycleRegisteredEvent(id, motorcycle.Identifier, motorcycle.Year, 
                motorcycle.Model, motorcycle.Plate));

            return id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(id);

            if (motorcycle == null)
            {
                throw new NotFoundException("Motorcycle doesn't exist.");
            }

            if(!motorcycle.CanBeDeleted()){
                throw new DomainException("Motorcycle has rentals and cannot be deleted.");
            }

            await _motorcycleRepository.RemoveAsync(motorcycle);
        }

        public async Task<MotorcycleViewModel> GetByIdAsync(Guid id)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(id);

            if (motorcycle == null)
            {
                throw new NotFoundException("Motorcycle doesn't exist.");
            }

            return new MotorcycleViewModel(motorcycle.Id, motorcycle.Identifier, motorcycle.Year, motorcycle.Model, motorcycle.Plate);
        }

        public async Task<IEnumerable<MotorcycleViewModel>> GetByPlateAsync(string? plate)
        {
            var motorcycles = await _motorcycleRepository.GetByPlateAsync(plate);

            return motorcycles.Select(x => new MotorcycleViewModel(x.Id, x.Identifier, x.Year, x.Model, x.Plate));
        }

        public async Task UpdateAsync(Guid id, UpdateMotorcycleInputModel model)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(id);

            if(motorcycle == null)
            {
                throw new DomainException("Motorcycle doesn't exist.");
            }

            motorcycle.UpdatePlate(model.Plate);

            await _motorcycleRepository.UpdateAsync(motorcycle);
        }
    }
}
