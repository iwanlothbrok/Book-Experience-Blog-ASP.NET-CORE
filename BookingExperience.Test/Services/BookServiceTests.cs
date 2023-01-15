namespace BookingExperience.Test.Services
{
    using AutoMapper;
    using BookExperience.Core.Extensions;
    using BookExperience.Core.Services.Author;
    using BookExperience.Core.Services.Book;
    using BookExperience.Core.Services.Publisher;
    using BookExperience.Core.Services.WantedBooks;
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;
    using RentalCars.Test;

    public class BookServiceTests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;
        private ApplicationDbContext bookDb;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();
            serviceProvider = serviceCollection
            .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IdentityDbContext<ApplicationUser>, ApplicationDbContext>()
                .AddSingleton<IBookService, BookService>()
                .BuildServiceProvider();

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);

            bookDb = serviceProvider.GetService<ApplicationDbContext>()!;

            SeedDb();
        }

        [Test]
        public void FindBookShouldReturnNull()
        {
            //Arrange
            var fakeId = 1231;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.FindBook(fakeId), Is.Null);
        }

        [Test]
        public void FindBookShouldReturnBook()
        {
            //Arrange
            var validId = 1;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.FindBook(validId), Is.Not.Null);
        }
        [Test]
        public void AllGenresShouldReturnOnlyOneGenre()
        {
            //Arrange
            var validCount = 1;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.AllGenres(), Is.Not.Null);
            Assert.That(service.AllGenres().Count, Is.EqualTo(validCount));
        }

        [Test]
        public void GetLastThreeBooksShouldReturn2Books()
        {
            //Arrange
            var validCount = 2;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.GetLastThreeBooks(), Is.Not.Null);
            Assert.That(service.GetLastThreeBooks().Count, Is.EqualTo(validCount));
        }

        [Test]
        public void GetAllBooksDetailsShouldReturn2Books()
        {
            //Arrange
            var validCount = 2;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.GetAllBooksDetails(), Is.Not.Null);
            Assert.That(service.GetAllBooksDetails().Count, Is.EqualTo(validCount));
        }
        [Test]
        public void GetDetailsForBookByIdShouldReturnCorrectBook()
        {
            //Arrange
            var validId = 1;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.GetDetailsForBookById(validId), Is.Not.Null);
        }
        [Test]
        public void GetDetailsForBookByIdShouldReturnNull()
        {
            //Arrange
            var fakeId = 3123;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.GetDetailsForBookById(fakeId), Is.Null);
        } 
        [Test]
        public void ByUserShouldReturnNull()
        {
            //Arrange
            var fakeId = "asda";

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.ByUser(fakeId), Is.Empty);
        } 
        [Test]
        public void ByUserShouldReturnCorrectBooks()
        {
            //Arrange
            var userId = "249b1fe6-3667-43d5-9ac9-4de6a92d923a";
            var bookCount = 2;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.ByUser(userId), Is.Not.Null);
            Assert.That(service.ByUser(userId).Count, Is.EqualTo(bookCount));
        }
        [Test]
        public void AllTitlesShouldReturn2Titles()
        {
            //Arrange
            var bookCount = 2;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.AllTitles().Count, Is.EqualTo(bookCount));
        }
        [Test]
        public void DeleteShouldReturnFalse()
        {
            //Arrange
            var fakeBookId = 123123;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.Delete(fakeBookId), Is.False);
        }
        [Test]
        public void DeleteShouldReturnTrue()
        {
            //Arrange
            var fakeBookId = 1;

            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(service.Delete(fakeBookId), Is.True);
        } 
        [Test]
        public async Task EditShouldReturnZero()
        {
            //Arrange
            List<IFormFile> bookPhoto = new List<IFormFile>();


            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(await service.Edit(1, "harry o", bookPhoto, "as", 99, 123, true, "aa", "asda", "asda", ""), Is.Zero);
            Assert.That(await service.Edit(1, "harry o", bookPhoto, "as", 99, 123, true, null, null, "asda", ""), Is.Zero);
        }
        [Test]
        public async Task CreateShouldReturnZero()
        {
            //Arrange
            List<IFormFile> CarPhoto = new List<IFormFile>();


            //Act
            var authorService = new AuthorService(bookDb);
            var publisher = new PublisherService(bookDb);
            var service = new BookService(bookDb, authorService, publisher, mapper);

            //Assert
            Assert.That(await service.Create("harry o", "aa", "asda", "asda", CarPhoto, "as", 99, 123, true, "asda",null), Is.Zero);
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
                Pages = 450,
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
