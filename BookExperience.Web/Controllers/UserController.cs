namespace BookExperience.Web.Controllers
{
    using BookExperience.Core.Extensions;
    using BookExperience.Core.Services.ApplicationUser;
    using BookExperience.Core.Services.Book;
    using BookExperience.Core.Services.WantedBooks;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class UserController : BaseController
    {
        private IBookService bookService;
        private IApplicationUserService userService;
        private IWantedBooksService wantedBooks;

        public UserController(IApplicationUserService userService, IBookService bookService, IWantedBooksService wantedBooks)
        {
            this.userService = userService;
            this.bookService = bookService;
            this.wantedBooks = wantedBooks;
        }

        [HttpGet]
        public IActionResult AddAsWanted(int id)
        {
            Book? book = this.bookService.FindBook(id);
            ApplicationUser? user = this.userService.FindApplicationUserById(User.GetId());

            if (user is not null && book is not null)
            {
                this.wantedBooks.AddWantedBooks(book.Id, user.Id);
                //this.userService.AddBook(book, user);
                //this.bookService.AddUserInWanted(book, user);
            }
            TempData[GlobalMessageKey] = "You marked the book as wanted!";

            return RedirectToAction("All", "Book");
        }

        [HttpGet]
        public IActionResult RemoveFromWanted(int id)
        {
            var book = this.bookService.FindBook(id);
            var user = this.userService.FindApplicationUserById(User.GetId());

            if (user is not null && book is not null)
            {
                user.WantedBooks.Remove(book);
            }

            TempData[GlobalMessageKey] = "You remove the book from wanted!";

            return RedirectToAction("All", "Book");
        }

        [HttpGet]
        public IActionResult WantedBooks()
        => View();
    }
}
