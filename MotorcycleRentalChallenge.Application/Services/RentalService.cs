using MotorcycleRentalChallenge.Application.InputModel;
using MotorcycleRentalChallenge.Application.Interfaces;
using MotorcycleRentalChallenge.Application.ViewModel;
using MotorcycleRentalChallenge.Core.Exceptions;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;

namespace MotorcycleRentalChallenge.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IRentalPlanRepository _rentalPlanRepository;
        private readonly IDeliveryDriverRepository _deliveryDriverRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public RentalService(
            IRentalRepository rentalRepository, 
            IRentalPlanRepository rentalPlanRepository,
            IDeliveryDriverRepository deliveryDriverRepository,
            IMotorcycleRepository motorcycleRepository)
        {
            _rentalRepository = rentalRepository;
            _rentalPlanRepository = rentalPlanRepository;
            _deliveryDriverRepository = deliveryDriverRepository;
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<Guid> AddAsync(AddRentalInputModel model)
        {
            if (!Guid.TryParse(model.DeliveryDriverId, out var driverId))
            {
                throw new DomainException("Invalid Delivery Driver Id.");
            }

            if (!Guid.TryParse(model.MotorcycleId, out var motorcycleId))
            {
                throw new DomainException("Invalid Motorcycle Id.");
            }

            var rentalPlan = await _rentalPlanRepository.GetByRentalDays(model.RentalPlanDays);
            if (rentalPlan == null)
            {
                throw new NotFoundException($"There is no Rental Plan for {model.RentalPlanDays} days.");
            }

            var entity = model.ToEntity(rentalPlan);

            var driver = await _deliveryDriverRepository.GetByIdAsync(driverId);
            if (driver == null)
            {
                throw new NotFoundException($"There is no Delivery Driver with this Id.");
            }           

            var motorcycle = await _motorcycleRepository.GetByIdAsync(motorcycleId);
            if (motorcycle == null)
            {
                throw new NotFoundException($"There is no Motorcycle with this Id.");
            }            

            return await _rentalRepository.AddAsync(entity);            
        }

        public Task<RentalViewModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, UpdateRentalInputModel model)
        {
            throw new NotImplementedException();
        }

    }
}
