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
            builder
              .Entity<Book>()
              .HasOne(c => c.Genres)
              .WithMany(c => c.Books)
              .HasForeignKey(c => c.GenresId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Book>()
              .HasOne(c => c.Author)
              .WithMany(d => d.Books)
              .HasForeignKey(c => c.AuthorId)
              .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }

        public DbSet<Genres> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors{ get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}
