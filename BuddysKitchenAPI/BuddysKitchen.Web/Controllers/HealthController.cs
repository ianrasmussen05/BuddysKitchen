using BuddysKitchen.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<RecipeController> Logger;
        private readonly IRecipeService RecipeService;

        public HealthController(ILogger<RecipeController> logger, IRecipeService recipeService)
        {
            Logger = logger;
            RecipeService = recipeService;
        }

        [HttpGet("health", Name = "get-health")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await RecipeService.Health();
                return Ok(new
                {
                    Message = "Healthy"
                });
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'api/health': {Message}", ex.Message);
                return BadRequest(new
                {
                    ex.Message
                });
            }
        }
    }
}
