using System.ComponentModel.DataAnnotations;

namespace BuddysKitchen.Entities
{
    public class Cuisine
    {
        public Cuisine()
        {
            Recipes = [];
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public ICollection<Recipe> Recipes;
    }
}
