namespace BookExperience.Core.Models.Books
{
    public class BookDetailsModel
    {
        public string Title { get; set; } = null!;
        public byte[]? BookPhoto { get; set; } = null!;
        public string PublisherName { get; set; } = null!;
        public string? Language { get; set; }
        public string GenresName { get; set; } = null!;
        public string AuthorFirstName { get; set; } = null!;
        public string AuthorLastName { get; set; } = null!;
        public int Pages { get; set; }
        public bool IsRecommended { get; set; }
    }
}
