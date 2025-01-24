using BuddysKitchen.Core.Enums;

namespace BuddysKitchen.Models
{
    public class RecipeModel
    {
        public RecipeModel()
        {
            RecipeIngredients = [];
            RecipeDirections = [];
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Servings { get; set; }
        public MealType? MealType { get; set; }
        public long? CuisineId { get; set; }

        public List<RecipeIngredientModel> RecipeIngredients { get; set; }
        public List<RecipeDirectionModel> RecipeDirections { get; set; }
    }
}
