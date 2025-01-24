using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IIngredientService IngredientService;

        public IngredientController(ILogger<RecipeController> logger, IIngredientService ingredientService)
        {
            _logger = logger;
            IngredientService = ingredientService;
        }

        [HttpGet("get-all", Name = "get-all-ingredients")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<IngredientModel> results = await IngredientService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'ingredient/get-all': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get", Name = "get-ingredient")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                IngredientModel? result = await IngredientService.GetAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'ingredient/get': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add", Name = "add-ingredient")]
        public async Task<IActionResult> Add(IngredientModel model)
        {
            try
            {
                IngredientModel result = await IngredientService.AddAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'ingredient/add': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update", Name = "update-ingredient")]
        public async Task<IActionResult> Update(IngredientModel model)
        {
            try
            {
                IngredientModel? result = await IngredientService.UpdateAsync(model);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'ingredient/update': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete", Name = "delete-ingredient")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                bool deleted = await IngredientService.DeleteAsync(id);
                if (!deleted)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'ingredient/delete': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
