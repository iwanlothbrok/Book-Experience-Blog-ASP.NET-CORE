namespace BookExperience.Core.Services.WantedBooks
{
    using BookExperience.Infrastrucutre.Data.Models;

    public interface IWantedBooksService
    {
        bool RemoveWantedBook(int id);
        bool AddWantedBooks(int bookId, string userId);
        List<WantedBook> GetWantedBooksByUserId(string id);
        bool DoesBookIsWantedByUser(int bookId, string userId);
    }
}
