namespace BookExperience.Core.Services.Genre
{
    using BookExperience.Core.Models.Genres;
    using BookExperience.Infrastrucutre.Data.Models;

    public interface IGenreService
    {
        Genres? FindGenre(int id);
        GenresFilterModel? GetBooksSortedByGenre(int id);
    }
}
