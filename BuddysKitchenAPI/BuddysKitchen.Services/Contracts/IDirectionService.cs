using BuddysKitchen.Models;

namespace BuddysKitchen.Services.Contracts
{
    public interface IDirectionService
    {
        Task<List<DirectionModel>> GetAllAsync();
        Task<DirectionModel?> GetAsync(long id);
        Task<DirectionModel> AddAsync(DirectionModel model);
        Task<DirectionModel?> UpdateAsync(DirectionModel model);
        Task<bool> DeleteAsync(long id);
    }
}
