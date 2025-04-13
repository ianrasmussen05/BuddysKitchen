using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> Logger;
        private readonly IConfiguration Configuration;
        private readonly IUserService UserService;

        public LoginController(ILogger<LoginController> logger, IConfiguration configuration, IUserService userService)
        {
            Logger = logger;
            Configuration = configuration;
            UserService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            try
            {
                var token = await UserService.LoginUser(model);
                if (token == null)
                    return Unauthorized("Invalid credentials.");
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'login': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
