using BuddysKitchen.Models;

namespace BuddysKitchen.Services.Contracts
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllAsync();
        Task<UserModel?> GetAsync(string email);
        Task<UserModel> AddAsync(UserModel model);
        Task<UserModel?> UpdateAsync(UserModel model);
        Task<bool> DeleteAsync(string email);
        Task<UserModel?> RegisterUser(RegisterUserModel model);
        Task<string?> LoginUser(LoginUserModel model);
    }
}
