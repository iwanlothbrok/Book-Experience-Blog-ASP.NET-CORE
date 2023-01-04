namespace BookExperience.Core.Models.Books
{
    public class BookQueryModel
    {
        public int CurrentPage { get; init; }

        public int BooksPerPage { get; init; }

        public int TotalBooks { get; init; }

        public IEnumerable<MineBooksModel>? Books { get; init; }
    }
}
