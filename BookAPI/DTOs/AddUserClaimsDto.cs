using System.ComponentModel.DataAnnotations;

namespace ContactBookAPI.DTOs
{
    public class AddUserClaimsDto
    {
        [Required]
        public string UserId { get; set; } = "";

        [Required]
        public Dictionary<string, string> ClaimsToAdd { get; set; } = new Dictionary<string, string>();
    }
}
