namespace BookExperience.Infrastructure.Seeding
{
    using BookExperience.Infrastrucutre.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using static BookExperience.Infrastructure.Seeding.SeedingConstants;
    public class RoleSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRole))
                        return;

                    var adminRole = new IdentityRole { Name = AdminRole };

                    await roleManager.CreateAsync(adminRole);

                    if (await roleManager.RoleExistsAsync(ManagerRole))
                        return;

                    var userRole = new IdentityRole { Name = ManagerRole };

                    await roleManager.CreateAsync(userRole);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
