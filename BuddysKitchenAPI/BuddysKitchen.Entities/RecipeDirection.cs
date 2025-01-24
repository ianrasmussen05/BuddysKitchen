using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuddysKitchen.Entities
{
    public class RecipeDirection
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; }
        public long RecipeId { get; set; }

        [ForeignKey(nameof(DirectionId))]
        public Direction Direction { get; set; }
        public long DirectionId { get; set; }
    }
}
