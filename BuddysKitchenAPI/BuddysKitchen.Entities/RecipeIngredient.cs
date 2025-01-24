using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuddysKitchen.Entities
{
    public class RecipeIngredient
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe? Recipe { get; set; }
        [Required]
        public long RecipeId { get; set; }

        [ForeignKey(nameof(IngredientId))]
        public Ingredient? Ingredient { get; set; }
        [Required]
        public long IngredientId { get; set; }

        [ForeignKey(nameof(IngredientImageId))]
        public IngredientImage? IngredientImage { get; set; }
        public long? IngredientImageId { get; set; }
    }
}
