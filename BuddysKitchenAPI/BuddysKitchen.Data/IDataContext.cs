using BuddysKitchen.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuddysKitchen.Data
{
    public interface IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<Recipe> Recipes { get; set; }
        DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<IngredientImage> IngredientsImage { get; set; }
        DbSet<RecipeDirection> RecipeDirection { get; set; }
        DbSet<Direction> Directions { get; set; }
        DbSet<Cuisine> Cuisines { get; set; }
    }
}
