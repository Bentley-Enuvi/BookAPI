using System.ComponentModel.DataAnnotations;

namespace BookAPI.DTOs
{
    public class RegistrationRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        
    }
}
