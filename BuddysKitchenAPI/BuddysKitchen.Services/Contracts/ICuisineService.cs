using BuddysKitchen.Models;

namespace BuddysKitchen.Services.Contracts
{
    public interface ICuisineService
    {
        Task<List<CuisineModel>> GetAllAsync();
        Task<CuisineModel?> GetAsync(long id);
        Task<CuisineModel> AddAsync(CuisineModel model);
        Task<CuisineModel?> UpdateAsync(CuisineModel model);
        Task<bool> DeleteAsync(long id);
    }
}
