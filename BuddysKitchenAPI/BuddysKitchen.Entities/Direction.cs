using System.ComponentModel.DataAnnotations;

namespace BuddysKitchen.Entities
{
    public class Direction
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public int StepNumber { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
    }
}
