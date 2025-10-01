using MotorcycleRentalChallenge.Core.Exceptions;
using MotorcycleRentalChallenge.Core.Interfaces.Storage;

namespace MotorcycleRentalChallenge.Infrastructure.Storage
{
    public class LocalStorageService : IFileStorageService
    {
        private readonly string _uploadsCnhFolder
            = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploadsCNH");
                
        private static string GetExtension(string metadata)
        {
            if (string.IsNullOrEmpty(metadata))
            {
                return "";
            }

            if (metadata.ToLowerInvariant().Contains("image/png"))
            {
                return ".png";
            }
            else if (metadata.ToLowerInvariant().Contains("image/bmp"))
            {
                return ".bmp";
            }
            else return "";
        }

        public void DeleteFile(string fileName)
        {
            string filePath = Path.Combine(_uploadsCnhFolder, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }            
        }

        public string GenerateFileName(string base64Content)
        {
            if (string.IsNullOrWhiteSpace(base64Content))
            {
                throw new DomainException("Invalid image.");
            }

            var base64Parts = base64Content.Split(',');
            var metadata = base64Parts.Length > 1 ? base64Parts[0] : string.Empty;

            var extension = GetExtension(metadata);

            return $"{Guid.NewGuid()}{extension}";
        }

        public async Task<string> UploadFileBase64Async(string content, string fileName)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new DomainException("Invalid image.");
            }

            if (!Directory.Exists(_uploadsCnhFolder))
            {
                Directory.CreateDirectory(_uploadsCnhFolder);
            }

            var base64Parts = content.Split(',');            
            var base64Data = base64Parts.Length > 1 ? base64Parts[1] : base64Parts[0];
                        
            var filePath = Path.Combine(_uploadsCnhFolder, fileName);

            var bytes = Convert.FromBase64String(base64Data);

            await File.WriteAllBytesAsync(filePath, bytes);

            return filePath;
        }
    }
}
