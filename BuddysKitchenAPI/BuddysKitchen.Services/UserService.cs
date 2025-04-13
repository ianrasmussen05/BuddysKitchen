using BuddysKitchen.Core;
using BuddysKitchen.Core.Enums;
using BuddysKitchen.Data;
using BuddysKitchen.Entities;
using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuddysKitchen.Services
{
    public class UserService : IUserService
    {
        private IDataContext DataContext { get; }
        private IConfiguration Configuration { get; }
        private PasswordHandler PasswordHandler { get; }

        public UserService(IDataContext dataContext, IConfiguration configuration)
        {
            DataContext = dataContext;
            Configuration = configuration;
            PasswordHandler = new PasswordHandler();
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            var entities = await DataContext.Users.ToListAsync();
            return entities.Select(c => c.Copy<User, UserModel>()).ToList();
        }

        public async Task<UserModel?> GetAsync(string email)
        {
            var entity = await DataContext.Users.Where(c => c.Email == email).FirstOrDefaultAsync();
            if (entity == null)
                return null;

            return entity.Copy<User, UserModel>();
        }

        public async Task<UserModel> AddAsync(UserModel model)
        {
            var entity = model.Copy<UserModel, User>();
            DataContext.Users.Add(entity);
            await DataContext.SaveChangesAsync();

            return entity.Copy<User, UserModel>();
        }

        public async Task<UserModel?> UpdateAsync(UserModel model)
        {
            var entity = DataContext.Users.Where(c => c.Email == model.Email).FirstOrDefault();
            if (entity == null)
                return null;

            // Update DB
            Map(entity, model);
            await DataContext.SaveChangesAsync();

            return entity.Copy<User, UserModel>();
        }

        public async Task<bool> DeleteAsync(string email)
        {
            var entity = DataContext.Users.FirstOrDefault(c => c.Email == email);
            if (entity == null)
                return false;

            DataContext.Users.Remove(entity);
            await DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserModel?> RegisterUser(RegisterUserModel model)
        {
            if (await DataContext.Users.AnyAsync(c => c.Email == model.Email))
                return null;

            PasswordHandler.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = Role.Creator,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            DataContext.Users.Add(user);
            await DataContext.SaveChangesAsync();

            return Copy(user);
        }

        public async Task<string?> LoginUser(LoginUserModel model)
        {
            var user = await DataContext.Users.FirstOrDefaultAsync(c => c.Email == model.Email);
            if (user == null || !PasswordHandler.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            // Generate JWT token
            var token = GenerateJwtToken(user);
            return token;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("FullName", $"{user.FirstName} {user.LastName}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Configuration["Jwt:Issuer"],
                audience: Configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static void Map(User entity, UserModel model)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.Role = model.Role;
        }

        private static UserModel Copy(User entity)
        {
            return new UserModel
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Role = entity.Role
            };
        }
    }
}
