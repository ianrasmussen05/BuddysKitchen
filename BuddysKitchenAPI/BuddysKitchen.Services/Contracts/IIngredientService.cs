using BuddysKitchen.Models;

namespace BuddysKitchen.Services.Contracts
{
    public interface IIngredientService
    {
        Task<List<IngredientModel>> GetAllAsync();
        Task<IngredientModel> GetAsync(long id);
        Task<IngredientModel> AddAsync(IngredientModel model);
        Task<IngredientModel?> UpdateAsync(IngredientModel model);
        Task<bool> DeleteAsync(long id);
    }
}
