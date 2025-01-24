using BuddysKitchen.Models;

namespace BuddysKitchen.Services.Contracts
{
    public interface IRecipeService
    {
        Task Health();
        Task<List<RecipeModel>> GetAllAsync();
        Task<RecipeModel?> GetAsync(long id);
        Task<RecipeModel?> SaveAsync(RecipeModel model);
        Task<bool> DeleteAsync(long id);
    }
}
