namespace BookExperience.Infrastrucutre.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public int? WantedBooksIds_Capacity { get; set; }
        public virtual List<Book> WantedBooks { get; set; } = new List<Book>();
    }
}
