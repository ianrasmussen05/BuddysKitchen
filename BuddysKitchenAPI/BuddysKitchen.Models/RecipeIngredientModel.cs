namespace BuddysKitchen.Models
{
    public class RecipeIngredientModel
    {
        public long Id { get; set; }
        public long RecipeId { get; set; }
        public IngredientModel? Ingredient { get; set; }
        public long? IngredientId { get; set; }
        public IngredientImageModel? IngredientImage { get; set; }
        public long? IngredientImageId { get; set; }
    }
}
