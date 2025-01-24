namespace BuddysKitchen.Models
{
    public class IngredientModel
    {
        public IngredientModel()
        {
            RecipeIngredients = [];
            IngredientImages = [];
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string? Quantity { get; set; }

        public List<RecipeIngredientModel> RecipeIngredients;
        public List<IngredientImageModel> IngredientImages;
    }
}
