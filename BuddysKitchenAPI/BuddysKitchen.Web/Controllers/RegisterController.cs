using Microsoft.AspNetCore.Mvc;
using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> Logger;
        private readonly IConfiguration Configuration;
        private readonly IUserService UserService;

        public RegisterController(ILogger<RegisterController> logger, IConfiguration configuration, IUserService userService)
        {
            Logger = logger;
            Configuration = configuration;
            UserService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            try
            {
                var register = await UserService.RegisterUser(model);
                if (register == null)
                    return Ok("Email already exists");

                return Ok(register);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'register': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
