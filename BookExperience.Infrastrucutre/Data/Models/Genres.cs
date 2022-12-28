namespace BookExperience.Infrastrucutre.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static BookExperience.Infrastrucutre.Data.DataConstants;
    public class Genres
    {
        public int Id { get; set; }

        /// <summary>
        /// genre name
        /// </summary>
        [StringLength(maximumLength: GenresMaxLength)]
        public string Name { get; set; } = null!;

        public List<Book>? Books{ get; set; }

    }
}
