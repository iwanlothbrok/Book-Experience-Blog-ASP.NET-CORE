namespace BookExperience.Core.Services.Book
{
    using BookExperience.Core.Extensions;
    using BookExperience.Core.Models.Books;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IBookService
    {
        bool Delete(int id);
        Book? FindBook(int id);
        IEnumerable<Genres> AllGenres();
        IEnumerable<string> AllTitles();
        BookDetailsModel? Details(int id);
        BookFormModel? GetBookById(int id);
        IEnumerable<BookDetailsModel> GetLastThreeBooks();
        IEnumerable<MineBooksModel> ByUser(string userId);
        IEnumerable<MineBooksModel> GetBooks(IQueryable<Book> bookQuery);
        BookQueryModel All(string title, string searchTerm, BookSorting sorting, int currentPage, int booksPerPage);
        Task<int> Edit(int id, string Title, IList<IFormFile> bookPhoto, string? Language, int GenresId, int Pages,
            bool IsRecomended, string authorFName, string authorLName, string publisherName);
        Task<int> Create(string Title, string AuthorFirstName, string AuthorLastName, string PublisherName,
        IList<IFormFile> bookPhoto, string? Language, int GenresId, int Pages, bool IsRecomended, string userId);
    }
}
