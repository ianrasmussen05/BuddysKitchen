using System.ComponentModel.DataAnnotations;

namespace BuddysKitchen.Entities
{
    public class Ingredient
    {
        public Ingredient()
        {
            RecipeIngredients = [];
            IngredientImages = [];
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Quantity { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients;
        public ICollection<IngredientImage> IngredientImages;
    }
}
