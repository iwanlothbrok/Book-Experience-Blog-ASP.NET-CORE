namespace BookExperience.Web.Controllers
{
    using AutoMapper;
    using BookExperience.Core.Extensions;
    using BookExperience.Core.Models;
    using BookExperience.Core.Services.Book;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class BookController : BaseController
    {
        private readonly IBookService bookService;
        private readonly IMapper mapper;
        public BookController(IBookService bookService, IMapper mapper)
        {
            this.bookService = bookService;
            this.mapper = mapper;
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
        public async Task<IActionResult> Add(BookFormModel book, List<IFormFile> BookPhoto)
        {
            if (BookPhoto == null || BookPhoto.Count == 0)
            {
                book.Genres = this.bookService.AllGenres();

                return View(book);
            }

            if (this.ModelState.IsValid == false)
            {
                book.Genres = this.bookService.AllGenres();

                return View(book);
            }

            await this.bookService.Create(book.Title, book.AuthorFirstName, book.AuthorLastName, book.PublisherName, BookPhoto, book.Language, book.GenresId, book.Pages, book.IsRecommended, User.GetId());

            TempData[GlobalMessageKey] = "Thank you for adding this book!";

            return RedirectToAction(nameof(Mine));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            BookDetailsModel? book = this.bookService.Details(id);

            if (book == null)
            {
                return RedirectToAction("Error", "Home");
            }

            BookFormModel bookForm = this.mapper.Map<BookFormModel>(book);
            bookForm.Genres = this.bookService.AllGenres();

            return View(bookForm);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            BookDetailsModel car = this.bookService.Details(id);

            if (this.ModelState.IsValid == false)
            {
                return RedirectToAction("Error", "Home");
            }

            BookDetailsModel carForm = this.mapper.Map<BookDetailsModel>(car);

            return View(carForm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookFormModel book, List<IFormFile> BookPhoto)
        {
            if (BookPhoto == null || BookPhoto.Count == 0)
            {
                book.Genres = this.bookService.AllGenres();

                return View(book);
            }

            if (this.ModelState.IsValid == false)
            {
                book.Genres = this.bookService.AllGenres();

                return View(book);
            }

            await this.bookService.Edit(
                id,
                book.Title,
                BookPhoto,
                book.Language,
                book.GenresId,
                book.Pages,
                book.IsRecommended,
                book.AuthorFirstName,
                book.AuthorLastName,
                book.PublisherName);


            TempData[GlobalMessageKey] = "You edit this book successfully!";

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public IActionResult Mine()
        {
            IEnumerable<MineBooksModel> myBooks = this.bookService
                .ByUser(this.User.GetId())
                .ToList();

            return View(myBooks);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            bool findBook = this.bookService.Delete(id);

            if (findBook == true)
            {
                TempData[GlobalMessageKey] = "You delete this book successfully!";
            }

            return RedirectToAction(nameof(Mine));
        }
    }
}
