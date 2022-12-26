namespace BookExperience.Infrastructure.Seeding
{
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CategorySeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Genres.Any(d => d.Id == 1))
            {
                var category1 = AddCategoryInDb(data, 1, "Adventure stories");
                var category2 = AddCategoryInDb(data, 2, "Classics");
                var category3 = AddCategoryInDb(data, 3, "Crime");
                var category4 = AddCategoryInDb(data, 4, "Fairy tales");
                var category5 = AddCategoryInDb(data, 5, "Fantasy");
                var category6 = AddCategoryInDb(data, 6, "Historical fiction");
                var category7 = AddCategoryInDb(data, 7, "Horror");
                var category8 = AddCategoryInDb(data, 8, "Humour and satire");
                var category9 = AddCategoryInDb(data, 9, "Philosophy");

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
