namespace BookExperience.Infrastrucutre.Data
{
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder
            //  .Entity<Car>() CHANGE IT WITH BOOK
            //  .HasOne(c => c.Category) 
            //  .WithMany(c => c.Cars)
            //  .HasForeignKey(c => c.CategoryId)
            //  .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; set; }
    }
}
