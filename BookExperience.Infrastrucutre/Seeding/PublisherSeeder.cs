namespace BookExperience.Infrastrucutre.Seeding
{
    using BookExperience.Infrastructure.Seeding;
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class PublisherSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Publishers.Any(d => d.Id == 1))
            {
                AddPublisher(data, 1, "Ciela");
                AddPublisher(data, 2, "Libri");

                data.Database.OpenConnection();
                try
                {
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Publishers ON");
                    data.SaveChanges();
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Publishers OFF");
                }
                finally
                {
                    data.Database.CloseConnection();
                }
            }
        }

        private Publisher AddPublisher
            (ApplicationDbContext data, int id, string name)
        {
            Publisher publisher = new Publisher()
            {
                Id = id,
                Name = name
            };

            data.Publishers.Add(publisher);

            return publisher;
        }
    }
}


