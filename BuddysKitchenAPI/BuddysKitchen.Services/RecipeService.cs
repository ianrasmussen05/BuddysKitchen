using BuddysKitchen.Core;
using BuddysKitchen.Data;
using BuddysKitchen.Entities;
using BuddysKitchen.Models;
using BuddysKitchen.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BuddysKitchen.Services
{
    public class RecipeService : IRecipeService
    {
        private IDataContext DataContext { get; }

        public RecipeService(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task Health()
        {
            await DataContext.Recipes.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get all recipes from the DB. Includes the cuisine, ingredients, and directions.
        /// </summary>
        /// <returns>List of RecipeModel's</returns>
        public async Task<List<RecipeModel>> GetAllAsync()
        {
            List<RecipeModel> results = [];

            var entities = await DataContext.Recipes
                .Include(r => r.Cuisine)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.IngredientImage)
                .Include(r => r.RecipeDirections)
                    .ThenInclude(rd => rd.Direction)
                .ToListAsync();

            // Copy db items to models
            ICollection<RecipeModel> models = entities.Select(r => r.Copy<Recipe, RecipeModel>()).ToList();

            foreach (var currEntity in entities)
            {
                // Get the current model from returned collection
                var currModel = models.Where(r => r.Id == currEntity.Id).FirstOrDefault();
                if (currModel == null)
                    continue;

                // Map the included objects from the query to the model object
                Map(currModel, currEntity);

                // Add to results list
                results.Add(currModel);
            }

            return results;
        }

        /// <summary>
        /// Get a recipe from the DB based on the Id. Includes the cuisine, ingredients, 
        /// and directions.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>RecipeModel</returns>
        public async Task<RecipeModel?> GetAsync(long id)
        {
            var entity = await DataContext.Recipes
                .Include(r => r.Cuisine)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.IngredientImage)
                .Include(r => r.RecipeDirections)
                    .ThenInclude(rd => rd.Direction)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (entity == null)
                return null;

            // Copy to model
            RecipeModel? recipeModel = entity.Copy<Recipe, RecipeModel>();

            Map(recipeModel, entity);

            return recipeModel;
        }

        public async Task<RecipeModel?> SaveAsync(RecipeModel model)
        {
            if (model.Id == 0) // new Recipe
                return await AddAsync(model);
            else // existing Recipe
                return await UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = DataContext.Recipes
                .Include(r => r.Cuisine)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.IngredientImage)
                .Include(r => r.RecipeDirections)
                    .ThenInclude(rd => rd.Direction)
                .Where(r => r.Id == id)
                .FirstOrDefault();
            if (entity == null)
                return false;

            DataContext.Recipes.Remove(entity);
            await DataContext.SaveChangesAsync();
            return true;
        }

        private async Task<RecipeModel> AddAsync(RecipeModel model)
        {
            // Get copy of model to DB entity
            var entity = model.Copy<RecipeModel, Recipe>();
            Map(entity, model);

            // Add to DB
            DataContext.Recipes.Add(entity);
            await DataContext.SaveChangesAsync();

            // Copy entity to model to return
            model = entity.Copy<Recipe, RecipeModel>();
            Map(model, entity);

            return model;
        }

        private async Task<RecipeModel?> UpdateAsync(RecipeModel model)
        {
            var entity = DataContext.Recipes
                .Include(r => r.Cuisine)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.IngredientImage)
                .Include(r => r.RecipeDirections)
                    .ThenInclude(rd => rd.Direction)
                .Where(r => r.Id == model.Id)
                .FirstOrDefault();

            if (entity == null)
                return null;

            // Update to DB
            Map(entity, model);
            await DataContext.SaveChangesAsync();

            // Return model obj
            Map(model, entity);
            return model;
        }

        /// <summary>
        /// Function that maps the recipe's ingredients and directions from a DB entity
        /// to a model that can be returned to the user.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dbItem"></param>
        private static void Map(RecipeModel model, Recipe dbItem)
        {            
            if (dbItem.RecipeIngredients.Count != 0)
            {
                model.RecipeIngredients.Clear(); // clear before adding
                foreach (var currRecipeIngredient in dbItem.RecipeIngredients)
                {
                    RecipeIngredientModel riModel = new()
                    {
                        Id = currRecipeIngredient.Id,
                        RecipeId = currRecipeIngredient.RecipeId,
                        IngredientId = currRecipeIngredient.IngredientId,
                        Ingredient = currRecipeIngredient.Ingredient.Copy<Ingredient, IngredientModel>(),
                        IngredientImageId = currRecipeIngredient.IngredientImageId,
                        IngredientImage = currRecipeIngredient.IngredientImage.Copy<IngredientImage, IngredientImageModel>()
                    };

                    model.RecipeIngredients.Add(riModel);
                }
            }

            if (dbItem.RecipeDirections.Count != 0)
            {
                model.RecipeDirections.Clear(); // clear before adding
                foreach (var currRecipeDirection in dbItem.RecipeDirections)
                {
                    RecipeDirectionModel rdModel = new()
                    {
                        Id = currRecipeDirection.Id,
                        RecipeId = currRecipeDirection.RecipeId,
                        DirectionId = currRecipeDirection.DirectionId,
                        Direction = currRecipeDirection.Direction.Copy<Direction, DirectionModel>()
                    };

                    model.RecipeDirections.Add(rdModel);
                }
            }
        }

        /// <summary>
        /// Function that maps the recipe's ingredients and directions from a model object
        /// to a DB entity that can be returned to the user.
        /// </summary>
        /// <param name="dbItem"></param>
        /// <param name="model"></param>
        private async void Map(Recipe dbItem, RecipeModel model)
        {
            dbItem.Name = model.Name;
            dbItem.Description = model.Description;
            dbItem.Servings = model.Servings;
            dbItem.MealType = model.MealType;
            dbItem.CuisineId = model.CuisineId;

            // Update ReciperIngredients
            dbItem.RecipeIngredients.Clear();
            foreach (var currRecipeIngredient in model.RecipeIngredients)
            {
                RecipeIngredient riItem = new()
                {
                    Id = currRecipeIngredient.Id,
                    RecipeId = currRecipeIngredient.RecipeId,
                    IngredientId = currRecipeIngredient.IngredientId ?? 0,
                    Ingredient = currRecipeIngredient.Ingredient?.Copy<IngredientModel, Ingredient>(),
                    IngredientImageId = currRecipeIngredient.IngredientImageId == 0 ? null : currRecipeIngredient.IngredientImageId,
                    IngredientImage = currRecipeIngredient.IngredientImageId == 0 ? null : currRecipeIngredient.IngredientImage?.Copy<IngredientImageModel, IngredientImage>()
                };

                dbItem.RecipeIngredients.Add(riItem);
            }

            // Update RecipeDirections
            dbItem.RecipeDirections.Clear();
            foreach (var currRecipeDirection in model.RecipeDirections)
            {
                RecipeDirection rdItem = new()
                {
                    Id = currRecipeDirection.Id,
                    RecipeId = currRecipeDirection.RecipeId,
                    DirectionId = currRecipeDirection.DirectionId ?? 0,
                    Direction = currRecipeDirection.DirectionId == 0 ? null : await DataContext.Directions.Where(d => d.Id == currRecipeDirection.DirectionId).FirstOrDefaultAsync()
                };

                if (currRecipeDirection.DirectionId == 0 && currRecipeDirection.Direction != null)
                {
                    Direction currDirection = new()
                    {
                        Id = currRecipeDirection.DirectionId ?? 0,
                        StepNumber = currRecipeDirection.Direction?.StepNumber ?? 0,
                        Description = currRecipeDirection.Direction?.Description
                    };
                    rdItem.Direction = currDirection;
                }

                dbItem.RecipeDirections.Add(rdItem);
            }
        }
    }
}
