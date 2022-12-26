namespace BookExperience.Core.Services.Author
{
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;

    public class AuthorService : IAuthorService
	{
        private readonly ApplicationDbContext data;
        public AuthorService(ApplicationDbContext data)
        {
            this.data = data;
        }
        public int Create(string firstName, string lastName)
        {
            Author author = new Author()
            {
                FirstName = firstName,
                LastName = lastName
            };

            this.data.Authors.Add(author);
            this.data.SaveChanges();

            return author.Id;
        }
    }
}
