namespace BookExperience.Core.Models
{
    public class MineBooksModel
    {
        public string Title { get; set; } = null!;
        public string AuthorFirstName { get; set; } = null!;
        public string AuthorLastName { get; set; } = null!;
        public byte[]? BookPhoto { get; set; } = null!;
        public string PublisherName { get; set; } = null!;
        public string GenresName { get; set; } = null!;
        public string? Language { get; set; }
        public int Pages { get; set; }
        public bool IsRecommended { get; set; }
    }
}
