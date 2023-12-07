using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookAPI.Data.Entities
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";


        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
