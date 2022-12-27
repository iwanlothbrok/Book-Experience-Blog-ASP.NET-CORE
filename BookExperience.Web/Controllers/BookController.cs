namespace BookExperience.Web.Controllers
{
    using BookExperience.Core.Extensions;
    using BookExperience.Core.Models;
    using BookExperience.Core.Services.Book;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class BookController : BaseController
    {
        private readonly IBookService bookService;
        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new BookFormModel
            {
                Genres = this.bookService.AllGenres()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookFormModel book, List<IFormFile> Photo)
        {
            if (Photo == null || Photo.Count == 0)
            {
                book.Genres = this.bookService.AllGenres();

                return View(book);
            }

            if (this.ModelState.IsValid == false)
            {
                book.Genres = this.bookService.AllGenres();

                return View(book);
            }

            await this.bookService.Create(book.Title, book.AuthorFirstName, book.AuthorLastName, book.PublisherName, Photo, book.Language, book.GenresId, book.Pages, book.IsRecommended, User.GetId());

            TempData[GlobalMessageKey] = "Thank you for adding your car!";

            return RedirectToAction(nameof(Add));
        }

        [HttpGet]
        public IActionResult Mine()
        {
            IEnumerable<MineBooksModel> myBooks = this.bookService
                .ByUser(this.User.GetId())
                .ToList();

            return View(myBooks);
        }
    }
}
