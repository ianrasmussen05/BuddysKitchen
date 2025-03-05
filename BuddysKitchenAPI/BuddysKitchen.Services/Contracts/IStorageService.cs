using Microsoft.AspNetCore.Http;

namespace BuddysKitchen.Services.Contracts
{
    public interface IStorageService
    {
        Task<string> UploadFile(string containerName, IFormFile file);
        Task<bool> DeleteFile(string containerName, string blobName);
        byte[] DownloadFile(string containerName, string blobName);
    }
}
