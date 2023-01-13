namespace BookExperience.Infrastrucutre.Data.Models
{
    using ServiceStack.DataAnnotations;

    public class WantedBook
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
