using System.ComponentModel.DataAnnotations;

namespace BookAPI.DTOs
{
    public class AddBookDto
    {
        [Required]
        public string Title { get; set; } = "";

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "A minimum of three characters is required!")]
        public string AuthorFirstName { get; set; } = "";

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "A minimum of three characters is required!")]
        public string AuthorLastName { get; set; } = string.Empty;
        public string Description { get; set; } = "";
        public int PageCount { get; set; }

        [Required]
        public string DatePublished { get; set; }
    }
}
