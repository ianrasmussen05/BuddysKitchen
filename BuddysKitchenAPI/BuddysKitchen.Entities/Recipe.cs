using BuddysKitchen.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuddysKitchen.Entities
{
    public class Recipe
    {
        public Recipe() 
        {
            RecipeIngredients = [];
            RecipeDirections = [];
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(20)]
        public string? Servings { get; set; }
        public MealType? MealType { get; set; }

        [ForeignKey(nameof(CuisineId))]
        public Cuisine? Cuisine { get; set; }
        public long? CuisineId { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients;
        public ICollection<RecipeDirection> RecipeDirections;
    }
}
