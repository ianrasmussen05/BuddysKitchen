using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController : ControllerBase
    {
        private readonly ILogger<DirectionController> _logger;
        private readonly IDirectionService DirectionService;

        public DirectionController(ILogger<DirectionController> logger, IDirectionService directionService)
        {
            _logger = logger;
            DirectionService = directionService;
        }

        [HttpGet("get-all", Name = "get-all-directions")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<DirectionModel> results = await DirectionService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'direction/get-all': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get", Name = "get-direction")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                DirectionModel? result = await DirectionService.GetAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'direction/get': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add", Name = "add-direction")]
        public async Task<IActionResult> Add(DirectionModel model)
        {
            try
            {
                DirectionModel result = await DirectionService.AddAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'direction/add': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update", Name = "update-direction")]
        public async Task<IActionResult> Update(DirectionModel model)
        {
            try
            {
                DirectionModel? result = await DirectionService.UpdateAsync(model);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'direction/update': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete", Name = "delete-direction")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                bool deleted = await DirectionService.DeleteAsync(id);
                if (!deleted)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'direction/delete': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
