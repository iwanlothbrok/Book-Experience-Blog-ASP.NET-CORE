namespace BookExperience.Core.Models.Genres
{
    using BookExperience.Infrastrucutre.Data.Models;

    public class GenresFilterModel
    {
        public int Id { get; set; }
        public List<Book> SortedBooks{ get; set; } = new List<Book>();  
    }
}
