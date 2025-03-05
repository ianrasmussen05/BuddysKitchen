using BuddysKitchen.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly ILogger<CuisineController> _logger;
        private readonly IStorageService StorageService;

        public BlobController(ILogger<CuisineController> logger, IStorageService storageService)
        {
            _logger = logger;
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
                _logger.LogError("Error on webservice 'recipe/get': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
