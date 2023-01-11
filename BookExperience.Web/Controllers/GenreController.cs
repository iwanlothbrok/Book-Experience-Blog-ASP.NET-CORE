namespace BookExperience.Web.Controllers
{
    using BookExperience.Core.Models.Genres;
    using BookExperience.Core.Services.Genre;
    using Microsoft.AspNetCore.Mvc;

    public class GenreController : Controller
    {
        private readonly IGenreService genreService;
        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }
        public IActionResult FilteredByGenres(int id)
        {
            GenresFilterModel model = new GenresFilterModel();

            if (id < 0)
            {
                return RedirectToAction("Error", "Home");
            }

            model = this.genreService.GetBooksSortedByGenre(id);

            if (model == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(model); 
        }
    }
}