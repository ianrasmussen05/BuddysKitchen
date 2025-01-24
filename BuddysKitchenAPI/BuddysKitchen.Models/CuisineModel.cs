namespace BuddysKitchen.Models
{
    public class CuisineModel
    {
        public CuisineModel()
        {
            Recipes = [];
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<RecipeModel> Recipes;
    }
}
