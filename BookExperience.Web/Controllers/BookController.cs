namespace BookExperience.Web.Controllers
{
    using BookExperience.Core.Models;
    using BookExperience.Core.Services.Book;
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
        public async Task<IActionResult> Add(BookFormModel book, List<IFormFile> bookPhoto)
        {
            if (bookPhoto == null || bookPhoto.Count == 0)
            {
                book.Genres = this.bookService.AllGenres();

                return View(book);
            }

            if (this.ModelState.IsValid == false)
            {
                book.Genres = this.bookService.AllGenres();

                return View(book);
            }

            await this.bookService.Create(book.Title, book.AuthorFirstName, book.AuthorLastName, book.PublisherName, bookPhoto, book.Language, book.GenresId,book.Pages ,book.IsRecomended);

            TempData[GlobalMessageKey] = "Thank you for adding your car!";

            return RedirectToAction(nameof(Add));
        }

    }
}
