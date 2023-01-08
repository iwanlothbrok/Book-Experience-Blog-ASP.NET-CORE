namespace BookExperience.Web.Controllers
{
    using BookExperience.Core.Extensions;
    using BookExperience.Core.Services.ApplicationUser;
    using BookExperience.Core.Services.Book;
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class UserController : BaseController
    {
        private IBookService bookService;
        private IApplicationUserService userService;
        private ApplicationDbContext data;
        public UserController(IApplicationUserService userService, IBookService bookService, ApplicationDbContext data)
        {
            this.userService = userService;
            this.bookService = bookService;
            this.data = data;
        }

        [HttpGet]
        public IActionResult AddAsWanted(int id)
        {
            Book? book = this.bookService.FindBook(id);
            ApplicationUser? user = this.userService.FindApplicationUserById(User.GetId());

            if (user is not null && book is not null)
            {
                user.WantedBooks.Add(book);
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
                this.data.SaveChanges();
            }

            TempData[GlobalMessageKey] = "You remove the book from wanted!";

            return RedirectToAction("All", "Book");
        }

        [HttpGet]
        public IActionResult WantedBooks()
        => View();
    }
}
