namespace BookExperience.Infrastructure.Seeding
{
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class CategorySeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Genres.Any(d => d.Id == 1))
            {
                var category1 = AddCategoryInDb(data, AdventureStoriesId, "Adventure stories");
                var category2 = AddCategoryInDb(data, ClassicsId, "Classics");
                var category3 = AddCategoryInDb(data, CrimeId, "Crime");
                var category4 = AddCategoryInDb(data, FairyTalesId, "Fairy tales");
                var category5 = AddCategoryInDb(data, FantasyId, "Fantasy");
                var category6 = AddCategoryInDb(data, HistoricalFictionId, "Historical fiction");
                var category7 = AddCategoryInDb(data, HorrorId, "Horror");
                var category8 = AddCategoryInDb(data, HumourAndSatireId, "Humour and satire");
                var category9 = AddCategoryInDb(data, PhilosophyId, "Philosophy");

                data.Database.OpenConnection();
                try
                {
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Genres ON");
                    data.SaveChanges();
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Genres OFF");
                }
                finally
                {
                    data.Database.CloseConnection();
                }
            }
        }

        private Genres AddCategoryInDb
            (ApplicationDbContext data, int id, string name)
        {
            Genres category = new Genres()
            {
                Id = id,
                Name = name,
            };

            data.Genres.Add(category);

            return category;
        }
    }
}
