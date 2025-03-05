using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BuddysKitchen.Services
{
    public class StorageService : IStorageService
    {
        private readonly IConfiguration Configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StorageService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> UploadFile(string containerName, IFormFile file)
        {
            try
            {
                var container = new BlobContainerClient(Configuration["Storage:BlobStorage:ConnectionString"], containerName);
                await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

                var blob = container.GetBlobClient(file.FileName);
                using (var fileStream = file.OpenReadStream())
                {
                    await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                }

                return blob.Uri.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteFile(string containerName, string blobName)
        {
            try
            {
                var container = new BlobContainerClient(Configuration["Storage:BlobStorage:ConnectionString"], containerName);
                var blob = container.GetBlobClient(blobName);
                await blob.DeleteIfExistsAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public byte[] DownloadFile(string containerName, string blobName)
        {
            try
            {
                var blobServiceClient = new BlobServiceClient(Configuration["Storage:BlobStorage:ConnectionString"]);
                var container = blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = container.GetBlobClient(blobName);

                using var memoryStream = new MemoryStream();
                blobClient.DownloadTo(memoryStream);
                return memoryStream.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
