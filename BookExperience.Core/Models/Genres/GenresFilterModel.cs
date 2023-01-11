namespace BookExperience.Core.Models.Genres
{
    using BookExperience.Core.Models.Books;

    public class GenresFilterModel
    {
        public int Id { get; set; }
        public List<BookDetailsModel> SortedBooks{ get; set; } = new List<BookDetailsModel>();  
    }
}
