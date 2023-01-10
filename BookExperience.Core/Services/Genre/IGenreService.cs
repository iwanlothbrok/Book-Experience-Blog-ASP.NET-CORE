namespace BookExperience.Core.Services.Genre
{
    using BookExperience.Core.Models.Genres;
    using BookExperience.Infrastrucutre.Data.Models;

    public interface IGenreService
    {
        GenresFilterModel? GetBooksSortedByGenre(int id);
    }
}
