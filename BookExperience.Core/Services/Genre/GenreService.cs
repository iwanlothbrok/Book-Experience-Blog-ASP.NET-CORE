namespace BookExperience.Core.Services.Genre
{
    using AutoMapper;
    using BookExperience.Core.Models.Genres;
    using BookExperience.Core.Services.Book;
    using BookExperience.Infrastrucutre.Data;

    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext data;
        private readonly IBookService bookService;
        private readonly IMapper mapper;
        public GenreService(ApplicationDbContext data, IBookService bookService, IMapper mapper)
        {
            this.data = data;
            this.bookService = bookService;
            this.mapper = mapper;
        }

        public GenresFilterModel? GetBooksSortedByGenre(int id)
        {
            GenresFilterModel model = new GenresFilterModel();
            if (this.data.Genres.Find(id) == null)
            {
                return null;
            }

            model.SortedBooks = this.data.Books.Where(c => c.GenresId == id).ToList();
            model.Id = id;

            return model;
        }
    }
}
