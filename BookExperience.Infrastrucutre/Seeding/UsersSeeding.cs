namespace BookExperience.Infrastructure.Seeding
{
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using static BookExperience.Infrastructure.Seeding.SeedingConstants;

    public class UsersSeeder : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            var emails = new List<string>() { userOneEmail, userTwoEmail };

            if (!data.Users.Any(u => u.Email == emails.First() && u.Email == emails.Last()))
            {
                var userManager =
                    serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                foreach (var email in emails)
                {
                    string password;
                    if (email == userOneEmail)
                    {
                        password = userOnePassword;
                    }
                    else
                    {
                        password = userTwoPassword;
                    }

                    Task.
                        Run(async () =>
                        {
                            var user = new ApplicationUser()
                            { Email = email, UserName = email };
                            await userManager.CreateAsync(user, password);
                        })
                        .GetAwaiter()
                        .GetResult();
                }
            }
        }
    }
}

