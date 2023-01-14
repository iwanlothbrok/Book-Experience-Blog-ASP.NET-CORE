namespace BookingExperience.Test.Services
{
    using BookExperience.Core.Services.WantedBooks;
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Test;

    public class WantedBooksServiceTests
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
                .AddSingleton<IWantedBooksService, WantedBooksService>()
                .BuildServiceProvider();


            bookDb = serviceProvider.GetService<ApplicationDbContext>()!;

            SeedDb();
        }

        [Test]
        public void TotalShouldBeEmpty()
        {
            //Arrange
            var fakeId = "a";

            //Act
            var service = new WantedBooksService(bookDb);

            //Assert
            Assert.That(service.GetWantedBooksByUserId(fakeId), Is.Empty);
        }

        [Test]
        public void TotalShouldReturnCorrectCounts()
        {
            //Arrange
            var Id = "iwaniwaniwaniwan";

            //Act
            var service = new WantedBooksService(bookDb);

            //Assert
            Assert.That(service.GetWantedBooksByUserId(Id).Count, Is.EqualTo(1));
        }

        [Test]
        public void AddWantedBooksShouldReturnFalse()
        {
            //Arrange
            var fakeBookId = 0;
            var notFakeBookId = 1;

          
            var notFakeUserId= "iwaniwaniwaniwan";

            //Act
            var service = new WantedBooksService(bookDb);

            //Assert
            Assert.That(service.AddWantedBooks(fakeBookId, notFakeUserId), Is.EqualTo(false));
            Assert.That(service.AddWantedBooks(notFakeBookId, null), Is.EqualTo(false));
        }
        
        [Test]
        public void AddWantedBooksShouldReturnTrue()
        {
            //Arrange
            var notFakeBookId = 1;
            var notFakeUserId = "iwaniwaniwaniwan";

            //Act
            var service = new WantedBooksService(bookDb);

            //Assert
            Assert.That(service.AddWantedBooks(notFakeBookId, notFakeUserId), Is.EqualTo(true));
        }

        [Test]
       public void DoesBookIsWantedByUserShouldReturnFalse()
        {
            //Arrange
            var notFakeBookId = 4;
            var notFakeUserId= "iwaniwaniwaniwan";

            //Act
            var service = new WantedBooksService(bookDb);

            //Assert
            Assert.That(service.DoesBookIsWantedByUser(notFakeBookId, notFakeUserId), Is.EqualTo(false));
        } 
       
        [Test]
        public void DoesBookIsWantedByUserShouldReturnTrue()
        {
            //Arrange
            var notFakeBookId = 2;
            var notFakeUserId= "iwaniwaniwaniwan";

            //Act
            var service = new WantedBooksService(bookDb);

            //Assert
            Assert.That(service.DoesBookIsWantedByUser(notFakeBookId, notFakeUserId), Is.EqualTo(true));
        } 
        
        [Test]
        public void RemoveWantedBookShouldReturnCorrectAnswers()
        {
            //Arrange
            var fakeId = 123;
            var id = 1;

            //Act
            var service = new WantedBooksService(bookDb);

            //Assert
            Assert.That(service.RemoveWantedBook(fakeId), Is.EqualTo(false));
            Assert.That(service.RemoveWantedBook(id), Is.EqualTo(true));
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
                Id = "opa123",
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
                Pages = 450,
                Description = "asdasdasdasdadasda",
                IsRecommended = true,
                Language = "ENG",
            };

            var wantedBook = new WantedBook()
            {
                Id = 1,
                BookId = book2.Id,
                ApplicationUserId= customer.Id,
            };
            bookDb.Users.AddRange(user, customer);
            bookDb.Genres.Add(genre);
            bookDb.Authors.Add(author);
            bookDb.Publishers.Add(publisher);
            bookDb.Books.AddRange(book, book2);
            bookDb.WantedBooks.Add(wantedBook);

            bookDb.SaveChanges();
        }
    }
}
