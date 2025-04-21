using BuddysKitchen.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly ILogger<CuisineController> Logger;
        private readonly IStorageService StorageService;

        public BlobController(ILogger<CuisineController> logger, IStorageService storageService)
        {
            Logger = logger;
            StorageService = storageService;
        }

        [HttpGet("get", Name = "get-blob")]
        public IActionResult Get(string containerName, string blobName)
        {
            try
            {
                var result = StorageService.DownloadFile(containerName, blobName);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'recipe/get': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("upload", Name = "upload-blob")]
        public async Task<IActionResult> Upload(string containerName, IFormFile file)
        {
            try
            {
                var result = await StorageService.UploadFile(containerName, file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'recipe/upload': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
