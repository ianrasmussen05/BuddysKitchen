namespace BuddysKitchen.Models
{
    public class RecipeDirectionModel
    {
        public long Id { get; set; }
        public long RecipeId { get; set; }
        public DirectionModel? Direction { get; set; }
        public long? DirectionId { get; set; }
    }
}
