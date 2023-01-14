namespace BookExperience.Core.Services.Genre
{
    using AutoMapper;
    using BookExperience.Core.Models.Genres;
    using BookExperience.Core.Services.Book;
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;

    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext data;
        private readonly IBookService bookService;
        public GenreService(ApplicationDbContext data, IBookService bookService)
        {
            this.data = data;
            this.bookService = bookService;
        }

        public Genres? FindGenre(int id)
        => this.data.Genres.Find(id);

        public GenresFilterModel? GetBooksSortedByGenre(int id)
        {
            GenresFilterModel model = new GenresFilterModel();
            Genres? genre = FindGenre(id);
            if (genre == null)
            {
                return null;
            }

            model.SortedBooks = this.bookService.GetAllBooksDetails().Where(c => c.GenresName.ToLower() == genre.Name.ToLower()).ToList();
            model.Id = id;

            return model;
        }
    }
}
