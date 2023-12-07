using System.ComponentModel.DataAnnotations;

namespace BookAPI.Data.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = "";
        public string AuthorFirstName { get; set; } = "";
        public string AuthorLastName { get; set; } = string.Empty;
        public string Description { get; set; } = "";
        public int PageCount { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
