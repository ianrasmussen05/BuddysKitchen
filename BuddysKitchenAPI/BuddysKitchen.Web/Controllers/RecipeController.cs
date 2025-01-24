using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeService RecipeService;

        public RecipeController(ILogger<RecipeController> logger, IRecipeService recipeService)
        {
            _logger = logger;
            RecipeService = recipeService;
        }

        [HttpGet("get-all", Name = "get-all-recipes")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<RecipeModel> results = await RecipeService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'recipe/get-all': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get", Name = "get-recipe")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                RecipeModel? result = await RecipeService.GetAsync(id);
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

        [HttpPost("save", Name = "save-recipe")]
        public async Task<IActionResult> Save(RecipeModel model)
        {
            try
            {
                RecipeModel? result = await RecipeService.SaveAsync(model);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'recipe/save': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete", Name = "delete-recipe")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                bool deleted = await RecipeService.DeleteAsync(id);
                if (!deleted)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on webservice 'recipe/delete': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
