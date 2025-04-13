using BuddysKitchen.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace BuddysKitchen.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(64)]
        public string LastName { get; set; }

        [Required]
        [StringLength(128)]
        public string Email { get; set; }

        [Required]
        public Role Role { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
