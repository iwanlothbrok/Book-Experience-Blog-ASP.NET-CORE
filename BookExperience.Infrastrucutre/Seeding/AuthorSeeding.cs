namespace BookExperience.Infrastrucutre.Seeding
{
    using BookExperience.Infrastructure.Seeding;
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class AuthorSeeding : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Authors.Any(d => d.Id == 77))
            {
                var dealerOne = AddAuthorInDb(data, 77, "J. K.", "Rowling");

                var dealerTwo = AddAuthorInDb(data, 78, "Paulo", "Coelho");

                data.Database.OpenConnection();
                try
                {
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Authors ON");
                    data.SaveChanges();
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Authors OFF");
                }
                finally
                {
                    data.Database.CloseConnection();
                }
            }
        }

        private Author AddAuthorInDb
            (ApplicationDbContext data, int id, string firstName, string lastName)
        {
            Author author = new Author()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };

            data.Authors.Add(author);

            return author;
        }
    }
}

