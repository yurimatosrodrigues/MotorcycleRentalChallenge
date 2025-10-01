namespace MotorcycleRentalChallenge.Core.Interfaces.Storage
{
    public interface IFileStorageService
    {
        string GenerateFileName(string base64Content);
        Task<string> UploadFileBase64Async(string content, string fileName);
        void DeleteFile(string fileName);
    }
}
