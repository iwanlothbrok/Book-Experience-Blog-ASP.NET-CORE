namespace BookExperience.Infrastrucutre.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class Publisher
    {
        public int Id { get; set; }

        /// <summary>
        /// publisher company
        /// </summary>
        [Required]
        [StringLength(maximumLength: PublisherNameMaxLength)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// books of this publisher
        /// </summary>
        public List<Book>? Books { get; set; }
    }
}
