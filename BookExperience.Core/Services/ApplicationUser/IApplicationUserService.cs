namespace BookExperience.Core.Services.ApplicationUser
{
    using BookExperience.Infrastrucutre.Data.Models;

    public interface IApplicationUserService
    {
        List<Book>? GetUserWantedBooks(string id);
        bool AddBook(Book book, ApplicationUser user);
        ApplicationUser? FindApplicationUserById(string id);
        bool UserHasThisBook(Book book, ApplicationUser user);

    }
}
