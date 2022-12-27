namespace BookExperience.Core.Models
{
    using BookExperience.Infrastrucutre.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class BookFormModel
    {
        [Required]
        [StringLength(maximumLength: BookTitleNameMaxLength)]
        [Display(Name = "Book Title")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: AuthorsLastNameMaxLength)]
        [Display(Name = "Author first name")]
        public string AuthorFirstName { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: AuthorsLastNameMaxLength)]
        [Display(Name = "Author last name")]

        public string AuthorLastName { get; set; } = null!;

        public byte[]? Photo { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: PublisherNameMaxLength)]
        [Display(Name = "Publisher name")]

        public string PublisherName { get; set; } = null!;

        public IEnumerable<Genres>? Genres { get; set; }
        public int GenresId { get; set; }

        /// <summary>
        /// book language
        /// </summary>
        [StringLength(maximumLength: LanguageNameMaxLength)]
        public string? Language { get; set; }

        /// <summary>
        /// book pages
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// if the book is recomended
        /// </summary>
        [Display(Name = "Do you recommend this book?")]
        [Required]
        public bool IsRecommended { get; set; }
    }
}
