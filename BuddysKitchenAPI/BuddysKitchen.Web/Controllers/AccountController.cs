using BuddysKitchen.Core.Enums;
using BuddysKitchen.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuddysKitchen.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> Logger;
        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SignInManager;
        private readonly IConfiguration Configuration;

        public AccountController(ILogger<AccountController> logger, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            Logger = logger;
            UserManager = userManager;
            SignInManager = signInManager;
            Configuration = configuration;
        }

        [HttpPost("register", Name = "register-user")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                var user = new User 
                { 
                    //UserName = model.Email, 
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Role = Role.User
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "user");
                    return Ok(new
                    {
                        Message = "User registered successfully"
                    });
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'account/register': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login", Name = "login-user")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null && await UserManager.CheckPasswordAsync(user, model.Password))
                {
                    var token = GenerateJwtToken(user);
                    return Ok(new
                    {
                        Token = token
                    });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on webservice 'account/login': {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private string GenerateJwtToken(User user)
        {
            return "";
            /*var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Convert int to string
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(Configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);*/
        }
    }

    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
