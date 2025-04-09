using BuddysKitchen.Core;
using BuddysKitchen.Data;
using BuddysKitchen.Entities;
using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuddysKitchen.Services
{
    public class UserService : IUserService
    {
        private IDataContext DataContext { get; }

        public UserService(IDataContext dataContext)
        {
            DataContext = dataContext;
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

        private static void Map(User entity, UserModel model)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.Role = model.Role;
        }
    }
}
