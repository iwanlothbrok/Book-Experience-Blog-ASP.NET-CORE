namespace BookExperience.Core.Models.Books
{
    using System.ComponentModel.DataAnnotations;

    public class BookDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public byte[]? BookPhoto { get; set; } = null!;
        public string PublisherName { get; set; } = null!;
        public string? Language { get; set; }
        public string GenresName { get; set; } = null!;

        [Display(Name = "User Feedback")]
        public string? Description { get; set; }
        public string AuthorFirstName { get; set; } = null!;
        public string AuthorLastName { get; set; } = null!;
        public int Pages { get; set; }
        public bool IsRecommended { get; set; }
        public string UserId { get; set; } = null!;
    }
}
