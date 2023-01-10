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
                return RedirectToAction("Error", "Index");
            }

            model = this.genreService.GetBooksSortedByGenre(id);

            return View(model); 
        }

        //public IActionResult AdventureStories()
        //{

        //}
    }
}
//Adventure stories
//Classics
//Crime
//Fairy tales
//Fantasy
//Historical fiction
//Horror
//Humour and satire
//Philosophy
//NULL