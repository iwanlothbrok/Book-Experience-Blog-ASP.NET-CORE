namespace BookExperience.Core.Services.ApplicationUser
{
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;

    public class ApplicationUserService : IApplicationUserService
    {
        private ApplicationDbContext data;
        public ApplicationUserService(ApplicationDbContext data)
        {
            this.data = data;
        }
        public ApplicationUser? FindApplicationUserById(string id)
        => this.data.ApplicationUsers.Find(id);
    }
}
