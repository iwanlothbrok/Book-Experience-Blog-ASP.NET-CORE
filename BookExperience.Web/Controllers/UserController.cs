namespace BookExperience.Web.Controllers
{
    using BookExperience.Core.Extensions;
    using BookExperience.Core.Services.ApplicationUser;
    using BookExperience.Core.Services.Book;
    using BookExperience.Infrastrucutre.Data;
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
            var book = this.bookService.FindBook(id);
            var user = this.userService.FindApplicationUserById(User.GetId());

            if (user is not null && book is not null)
            {
                user.WantedBooks.Add(book);
                this.data.SaveChanges();
            }

            TempData[GlobalMessageKey] = "You marked the book as wanted!";

            return RedirectToAction("All", "Book");
        }
    }
}
