namespace BookExperience.Infrastructure.Seeding
{
    using BookExperience.Infrastrucutre.Data;
    using System;

    public interface ISeeder
    {
        void Seed(ApplicationDbContext data, IServiceProvider serviceProvider);
    }
}
