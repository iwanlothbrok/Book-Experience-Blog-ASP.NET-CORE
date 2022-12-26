namespace BookExperience.Core.Services.Book
{
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IBookService
    {
        IEnumerable<Genres> AllGenres();
        Task<int> Create(string Title, string AuthorFirstName, string AuthorLastName, string PublisherName, IList<IFormFile> photo, string Language, int GenresId, int Pages, bool IsRecomended);
    }
}
