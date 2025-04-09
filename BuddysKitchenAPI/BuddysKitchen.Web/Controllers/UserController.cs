using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<RecipeController> Logger;
        private readonly IUserService UserService;

        public UserController(ILogger<RecipeController> logger, IUserService userService)
        {
            Logger = logger;
            UserService = userService;
        }

        [HttpGet("get", Name = "get-user")]
        public async Task<IActionResult> Get(string email)
        {
            try
            {
                UserModel? result = await UserService.GetAsync(email);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'user/get': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all", Name = "get-all-users")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<UserModel> results = await UserService.GetAllAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'user/get-all': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add", Name = "add-user")]
        public async Task<IActionResult> Add(UserModel model)
        {
            try
            {
                UserModel result = await UserService.AddAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'user/add': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update", Name = "update-user")]
        public async Task<IActionResult> Update(UserModel model)
        {
            try
            {
                UserModel? result = await UserService.UpdateAsync(model);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'user/update': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete", Name = "delete-user")]
        public async Task<IActionResult> Delete(string email)
        {
            try
            {
                bool result = await UserService.DeleteAsync(email);
                if (!result)
                    return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'user/delete': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
