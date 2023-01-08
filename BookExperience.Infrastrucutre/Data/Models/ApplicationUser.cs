namespace BookExperience.Infrastrucutre.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }
        public List<Book>? WantedBooks { get; set; } 
    }
}
