using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuisineController : ControllerBase
    {
        private readonly ILogger<CuisineController> _logger;
        private readonly ICuisineService CuisineService;

        public CuisineController(ILogger<CuisineController> logger, ICuisineService cuisineService)
        {
            _logger = logger;
            CuisineService = cuisineService;
        }

        [HttpGet("get-all", Name = "get-all-cuisines")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CuisineModel> results = await CuisineService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'cuisine/get-all': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get", Name = "get-cuisine")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                CuisineModel? result = await CuisineService.GetAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'cuisine/get': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add", Name = "add-cuisine")]
        public async Task<IActionResult> Add(CuisineModel model)
        {
            try
            {
                CuisineModel result = await CuisineService.AddAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'cuisine/add': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update", Name = "update-cuisine")]
        public async Task<IActionResult> Update(CuisineModel model)
        {
            try
            {
                CuisineModel? result = await CuisineService.UpdateAsync(model);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'cuisine/update': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete", Name = "delete-cuisine")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                bool deleted = await CuisineService.DeleteAsync(id);
                if (!deleted)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'cuisine/delete': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
