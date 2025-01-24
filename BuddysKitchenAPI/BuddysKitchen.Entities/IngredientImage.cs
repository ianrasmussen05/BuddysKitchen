using System.ComponentModel.DataAnnotations;

namespace BuddysKitchen.Entities
{
    public class IngredientImage
    {
        [Key]
        public long Id { get; set; }

        [StringLength(200)]
        public string? ImageURL { get; set; }
    }
}
