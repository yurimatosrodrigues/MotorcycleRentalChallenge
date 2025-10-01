using MotorcycleRentalChallenge.Application.InputModel;
using MotorcycleRentalChallenge.Application.Interfaces;
using MotorcycleRentalChallenge.Core.Exceptions;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;
using MotorcycleRentalChallenge.Core.Interfaces.Storage;

namespace MotorcycleRentalChallenge.Application.Services
{
    public class DeliveryDriverService : IDeliveryDriverService
    {
        private readonly IDeliveryDriverRepository _deliveryDriverRepository;
        private readonly IFileStorageService _fileStorageService;
        public DeliveryDriverService(IDeliveryDriverRepository deliveryDriverRepository,
            IFileStorageService fileStorageService)
        {
            _deliveryDriverRepository = deliveryDriverRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<Guid> AddAsync(AddDeliveryDriverInputModel model)
        {
            var base64Image = model.CnhImage;
            model.CnhImage = _fileStorageService.GenerateFileName(base64Image);

            var deliveryDriver = model.ToEntity();

            var driverCnhNumber = await _deliveryDriverRepository.GetByCnhNumberAsync(model.CnhNumber);

            if (driverCnhNumber != null) 
            {
                throw new DomainException("A delivery driver with this CNH number already exists.");
            }

            var driverCnpj = await _deliveryDriverRepository.GetByCnpjAsync(model.Cnpj);
            if (driverCnpj != null)
            {
                throw new DomainException("A delivery driver with this CNPJ already exists.");
            }

            await _fileStorageService.UploadFileBase64Async(base64Image, model.CnhImage);
            var id = await _deliveryDriverRepository.AddAsync(deliveryDriver);            

            return id;
        }

        public async Task SendCnhImageAsync(Guid id, CnhImageInputModel model)
        {
            var driver = await _deliveryDriverRepository.GetByIdAsync(id);
            if(driver == null)
            {
                throw new NotFoundException("Delivery driver doesn't exist.");
            }
            
            _fileStorageService.DeleteFile(driver.CnhImagePath);

            var base64Image = model.CnhImageBase64;
            var imageName = _fileStorageService.GenerateFileName(base64Image);
            
            driver.UpdateCnhImage(imageName);

            await _fileStorageService.UploadFileBase64Async(base64Image, imageName);
            await _deliveryDriverRepository.UpdateAsync(driver);            
        }
    }
}
