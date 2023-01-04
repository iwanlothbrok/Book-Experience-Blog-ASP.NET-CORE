namespace BookExperience.Core.Models.Books
{
    using BookExperience.Core.Extensions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllBooksQueryModel
    {
        public const int BooksPerPage = 3;

        public string Title { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public BookSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalBooks { get; set; }

        public IEnumerable<string> Titles { get; set; }

        public IEnumerable<MineBooksModel> Books { get; set; }
    }
}
