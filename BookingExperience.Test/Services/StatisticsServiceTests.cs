namespace BookingExperience.Test.Services
{
    using BookExperience.Core.Services.Author;
    using BookExperience.Core.Services.Statistics;
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Test;

    public class StatisticsServiceTests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private ApplicationDbContext bookDb;

        [SetUp]
        public void Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();
            serviceProvider = serviceCollection
            .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IdentityDbContext<ApplicationUser>, ApplicationDbContext>()
                .AddSingleton<IAuthorService, AuthorService>()
                .BuildServiceProvider();


            bookDb = serviceProvider.GetService<ApplicationDbContext>()!;

            SeedDb();
        }

        [Test]
        public void TotalShouldReturnCorrectCounts()
        {
            //Arrange
            var service = new StatisticsService(bookDb);

            //Act
            var result = service.Total();

            //Assert
            Assert.That(result.TotalBooksReaded, Is.EqualTo(2));
            Assert.That(result.TotalPagesReaded, Is.EqualTo(300));
            Assert.That(result.TotalGenresReaded, Is.EqualTo(1));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
        private void SeedDb()
        {
            var genre = new Genres()
            {
                Id = 99,
                Name = "Lux"
            };

            var user = new ApplicationUser()
            {
                Id = "249b1fe6-3667-43d5-9ac9-4de6a92d923a",
                PasswordHash = "1234",
                Email = "2123@abv.bg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            var customer = new ApplicationUser()
            {
                Id = "iwaniwaniwaniwan",
                PasswordHash = "1234",
                Email = "aasda@abv.bg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            var author = new Author()
            {
                Id = 99,
                FirstName = "J. K.",
                LastName = "Rowling",
            };
            var publisher = new Publisher()
            {
                Id = 99,
                Name = "Libri"
            };

            var book = new Book()
            {
                Id = 1,
                Title = "Harry Potter",
                BookPhoto = new byte[23123],
                AuthorId = author.Id,
                PublisherId = publisher.Id,
                UserId = user.Id,
                GenresId = genre.Id,
                Pages = 150,
                Description = "asdasdasdasdadasda",
                IsRecommended = false,
                Language = "BG",
            };
            var book2 = new Book()
            {
                Id = 2,
                Title = "Izmamna Realnost",
                BookPhoto = new byte[23123],
                AuthorId = author.Id,
                PublisherId = publisher.Id,
                UserId = user.Id,
                GenresId = genre.Id,
                Pages = 150,
                Description = "asdasdasdasdadasda",
                IsRecommended = true,
                Language = "ENG",
            };

            bookDb.Users.AddRange(user, customer);
            bookDb.Genres.Add(genre);
            bookDb.Authors.Add(author);
            bookDb.Publishers.Add(publisher);
            bookDb.Books.AddRange(book, book2);

            bookDb.SaveChanges();

        }
    }
}
