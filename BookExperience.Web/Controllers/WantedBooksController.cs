namespace BookExperience.Web.Controllers
{
    using BookExperience.Core.Extensions;
    using BookExperience.Core.Services.Book;
    using BookExperience.Core.Services.WantedBooks;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class WantedBooksController : BaseController
    {
        private IBookService bookService;
        private IWantedBooksService wantedBooks;

        public WantedBooksController(IBookService bookService, IWantedBooksService wantedBooks)
        {
            this.bookService = bookService;
            this.wantedBooks = wantedBooks;
        }

        [HttpGet]
        public IActionResult AddAsWanted(int id)
        {
            Book? book = this.bookService.FindBook(id);

            if (book is null)
            {
                return RedirectToAction("Error", "Home");
            }
            this.wantedBooks.AddWantedBooks(book.Id, User.GetId());
            TempData[GlobalMessageKey] = "You marked the book as wanted!";

            return RedirectToAction("All", "Book");
        }

        [HttpGet]
        public IActionResult RemoveFromWanted(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            this.wantedBooks.RemoveWantedBook(id);
            TempData[GlobalMessageKey] = "You remove the book from wanted!";

            return RedirectToAction("All", "Book");
        }

        [HttpGet]
        public IActionResult WantedBooks()
        => View();
    }
}
