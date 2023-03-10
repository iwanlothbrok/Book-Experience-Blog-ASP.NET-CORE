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
        public Author? GetAuthorInfo(int id)
        => this.data.Authors.Find(id);

        public int Create(string firstName, string lastName)
        {
            Author author = new Author()
            {
                FirstName = firstName,
                LastName = lastName
            };

            int didExists = DidAuthorExists(author.FirstName.ToLower(), author.LastName.ToLower());

            if (didExists > 0)
            {
                return didExists;
            }

            this.data.Authors.Add(author);
            this.data.SaveChanges();

            return author.Id;
        }

        public int DidAuthorExists(string firstName, string lastName)
        {
            List<Author> allAuthors = this.data.Authors.ToList();

            foreach (var author in allAuthors)
            {
                if (firstName.ToLower() == author.FirstName.ToLower() && lastName.ToLower() == author.LastName.ToLower())
                {
                    return author.Id;
                }
            }
            return 0;
        }

        public List<Author> GetAllAuthors()
        => this.data
            .Authors
            .OrderBy(n => n.FirstName)
            .ThenBy(l => l.LastName)
            .ToList();
    }
}
