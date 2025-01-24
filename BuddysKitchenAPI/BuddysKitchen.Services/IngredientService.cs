using BuddysKitchen.Core;
using BuddysKitchen.Data;
using BuddysKitchen.Entities;
using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuddysKitchen.Services
{
    public class IngredientService : IIngredientService
    {
        private IDataContext DataContext { get; }

        public IngredientService(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<List<IngredientModel>> GetAllAsync()
        {
            var entities = await DataContext.Ingredients.ToListAsync();
            return entities.Select(i => i.Copy<Ingredient, IngredientModel>()).ToList();
        }

        public async Task<IngredientModel> GetAsync(long id)
        {
            var entity = await DataContext.Ingredients.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (entity == null)
                return null;

            return entity.Copy<Ingredient, IngredientModel>();
        }

        public async Task<IngredientModel> AddAsync(IngredientModel model)
        {
            var entity = model.Copy<IngredientModel, Ingredient>();
            DataContext.Ingredients.Add(entity);
            await DataContext.SaveChangesAsync();

            return entity.Copy<Ingredient, IngredientModel>();
        }

        public async Task<IngredientModel?> UpdateAsync(IngredientModel model)
        {
            var entity = DataContext.Ingredients
                .Where(i => i.Id == model.Id)
                .FirstOrDefault();
            if (entity == null)
                return null;

            // Update DB entity
            Map(entity, model);
            await DataContext.SaveChangesAsync();

            return entity.Copy<Ingredient, IngredientModel>();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = DataContext.Ingredients.FirstOrDefault(r => r.Id == id);
            if (entity == null)
                return false;

            DataContext.Ingredients.Remove(entity);
            await DataContext.SaveChangesAsync();
            return true;
        }

        private static void Map(Ingredient dbItem, IngredientModel model)
        {
            dbItem.Id = model.Id;
            dbItem.Name = model.Name;
            //dbItem.ImageURL = model.ImageURL;
        }
    }
}
