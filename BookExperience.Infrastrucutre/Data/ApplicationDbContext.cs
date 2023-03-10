namespace BookExperience.Infrastrucutre.Data
{
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

            builder
            .Entity<Book>()
            .HasMany(c => c.UsersThatWantedTheBook)
            .WithMany(d => d.WantedBooks);


            base.OnModelCreating(builder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<WantedBook> WantedBooks { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
