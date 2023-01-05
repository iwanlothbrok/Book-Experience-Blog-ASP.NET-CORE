namespace BookExperience.Web.Controllers
{
    using BookExperience.Core.Models.Books;
    using BookExperience.Core.Services.Book;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class HomeController : Controller
    {
        public IBookService books;
        private readonly IMemoryCache cache;

        public HomeController(IBookService books,
            IMemoryCache cache)
        {
            this.books = books;
            this.cache = cache;

        }

        public IActionResult Index()
        {
            const string latestCarsCacheKey = "LatestCarsCacheKey";

            List<BookDetailsModel>? latestBooks = this.cache.Get<List<BookDetailsModel>>(latestCarsCacheKey);

            if (latestBooks == null)
            {
                latestBooks = this.books
                   .GetLastThreeBooks()
                   .ToList();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestCarsCacheKey, latestBooks, cacheOptions);
            }

            return View(latestBooks);
        }

        public IActionResult Error()
       => View();
    }

}
