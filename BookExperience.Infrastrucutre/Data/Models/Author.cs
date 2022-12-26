namespace BookExperience.Infrastrucutre.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class Author
    {
        public int Id { get; set; }

        /// <summary>
        /// authors first name
        /// </summary>
        [StringLength(maximumLength: AuthorsFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// authors last name
        /// </summary>
        [StringLength(maximumLength: AuthorsLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// books of this publisher
        /// </summary>
        public List<Book>? Books { get; set; }
    }
}
