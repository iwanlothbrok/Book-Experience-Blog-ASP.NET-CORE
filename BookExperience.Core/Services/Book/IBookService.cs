namespace BookExperience.Core.Services.Book
{
    using BookExperience.Core.Models;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IBookService
    {
        IEnumerable<Genres> AllGenres();
        IEnumerable<MineBooksModel> ByUser(string userId);
        IEnumerable<MineBooksModel> GetBooks(IQueryable<Book> bookQuery);
        Task<int> Create(string Title, string AuthorFirstName, string AuthorLastName, string PublisherName, IList<IFormFile> photo, string Language, int GenresId, int Pages, bool IsRecomended, string userId);
    }
}
