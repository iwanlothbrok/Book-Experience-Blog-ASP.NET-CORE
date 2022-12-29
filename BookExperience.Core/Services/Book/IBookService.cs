namespace BookExperience.Core.Services.Book
{
    using BookExperience.Core.Models;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IBookService
    {
        bool Delete(int id);
        IEnumerable<Genres> AllGenres();
        BookDetailsModel? Details(int id);
        BookFormModel? GetBookById(int id);
        IEnumerable<MineBooksModel> ByUser(string userId);
        IEnumerable<MineBooksModel> GetBooks(IQueryable<Book> bookQuery);
        Task<int> Edit(int id, string Title, IList<IFormFile> bookPhoto, string? Language, int GenresId, int Pages, bool IsRecomended);
        Task<int> Create(string Title, string AuthorFirstName, string AuthorLastName, string PublisherName,
        IList<IFormFile> bookPhoto, string? Language, int GenresId, int Pages, bool IsRecomended, string userId);
    }
}
