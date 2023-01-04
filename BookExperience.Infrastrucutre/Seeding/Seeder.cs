namespace BookExperience.Infrastructure.Seeding
{
    using BookExperience.Infrastrucutre.Data;

    public class Seeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var seeders = new List<ISeeder>()
            {
                new UsersSeeder(),
                new CategorySeeder(),
            };

            foreach (var seeder in seeders)
            {
                seeder.Seed(data, serviceProvider);
            }
        }
    }
}
