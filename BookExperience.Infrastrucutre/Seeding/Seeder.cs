namespace BookExperience.Infrastructure.Seeding
{
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Seeding;

    public class Seeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var seeders = new List<ISeeder>()
            {
                new UsersSeeder(),
                new AuthorSeeding(),
                new PublisherSeeder(),
                new CategorySeeder(),
                new BookSeeding()
            };

            foreach (var seeder in seeders)
            {
                seeder.Seed(data, serviceProvider);
            }
        }
    }
}
