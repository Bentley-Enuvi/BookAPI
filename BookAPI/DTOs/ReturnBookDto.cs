namespace BookAPI.DTOs
{
    public class ReturnBookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = "";
        public string AuthorFirstName { get; set; } = "";
        public string AuthorLastName { get; set; } = string.Empty;
        public string Description { get; set; } = "";
        public int PageCount { get; set; }
        public string DatePublished { get; set; } = "";
    }
}
