using System.ComponentModel.DataAnnotations;

namespace ContactBookAPI.DTOs
{
    public class AddUserRoleDto
    {
        [Required]
        public string UserId { get; set; } = "";

        [Required]
        public string Role { get; set; } = "";
    }
}
