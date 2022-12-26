namespace BookExperience.Infrastrucutre.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class Book
	{
		public int Id { get; set; }

		/// <summary>
		/// book title
		/// </summary>
		[Required]
        [StringLength(maximumLength: BookTitleNameMaxLength)]
        public string Title { get; set; } = null!;

		/// <summary>
		/// author of the book
		/// </summary>
		public int AuthorId { get; set; }
		public Author? Author { get; set; }

		/// <summary>
		/// publisher of the book
		/// </summary>
		public int? PublisherId { get; set; }
		public Publisher? Publisher { get; set; }

		/// <summary>
		/// category of the book
		/// </summary>
		public int GenresId { get; set; }
		public Genres? Genres { get; set; }

        /// <summary>
        /// book language
        /// </summary>
        [StringLength(maximumLength: LanguageNameMaxLength)]
        public string? Language { get; set; }

		/// <summary>
		/// photo of the book
		/// </summary>
		public byte[] BookPhoto { get; set; } = null!;

		/// <summary>
		/// book pages
		/// </summary>
		public int Pages{ get; set; }

		/// <summary>
		/// if the book is recomended
		/// </summary>
		public bool IsRecomended { get; set; }
	}
}
