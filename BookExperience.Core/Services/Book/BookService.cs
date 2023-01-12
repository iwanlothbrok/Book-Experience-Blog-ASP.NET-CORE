namespace BookExperience.Core.Services.Book
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookExperience.Core.Extensions;
    using BookExperience.Core.Models.Books;
    using BookExperience.Core.Services.Author;
    using BookExperience.Core.Services.Publisher;
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public class BookService : IBookService
    {
        private readonly ApplicationDbContext data;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;
        private readonly IMapper mapper;
        public BookService(ApplicationDbContext data, IAuthorService authorService, IPublisherService publisherService, IMapper mapper)
        {
            this.data = data;
            this.authorService = authorService;
            this.publisherService = publisherService;
            this.mapper = mapper;
        }
        public Book? FindBook(int id)
        => this.data.Books.Where(c => c.Id == id).FirstOrDefault();

        public IEnumerable<Genres> AllGenres()
            => this.data
                .Genres
                .Select(c => new Genres
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        public bool AddUserInWanted(Book book, ApplicationUser user)
        {
            if (book.UsersThatWantedTheBook.Contains(user))
            {
                return false;
            }
            book.UsersThatWantedTheBook.Add(user);
            this.data.SaveChanges();
            return true;
        }

        public IEnumerable<BookDetailsModel> GetLastThreeBooks()
        => this.data
           .Books
           .OrderByDescending(i => i.Id)
           .ProjectTo<BookDetailsModel>(this.mapper.ConfigurationProvider)
           .Take(3)
           .ToList();
        public IEnumerable<BookDetailsModel> GetAllBooksDetails()
        => this.data
           .Books
           .ProjectTo<BookDetailsModel>(this.mapper.ConfigurationProvider)
           .ToList();

        public BookDetailsModel? GetDetailsForBookById(int id)
       => this.data
           .Books
           .Where(i => i.Id == id)
           .ProjectTo<BookDetailsModel>(this.mapper.ConfigurationProvider)
           .FirstOrDefault();

        public BookFormModel? GetBookById(int id)
        => this.data
        .Books
         .Where(c => c.Id == id)
         .ProjectTo<BookFormModel>(this.mapper.ConfigurationProvider)
         .FirstOrDefault();

        public BookDetailsModel? Details(int id)
               => this.data
               .Books
                .Where(c => c.Id == id)
                .ProjectTo<BookDetailsModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();
        public IEnumerable<MineBooksModel> ByUser(string userId)
       => GetBooks(this.data
           .Books
           .Where(c => c.UserId == userId))
           .ToList();

        public IEnumerable<MineBooksModel> GetBooks(IQueryable<Book> bookQuery)
      => bookQuery
          .ProjectTo<MineBooksModel>(this.mapper.ConfigurationProvider)
          .ToList();

        public IEnumerable<string> AllTitles()
    => this.data
            .Books
            .Select(c => c.Title)
            .Distinct()
            .OrderBy(br => br)
            .ToList();

        public bool Delete(int id)
        {
            Book? bookData = FindBook(id);

            bool isValid = true;

            if (bookData == null)
            {
                isValid = false;
            }

            if (isValid == true)
            {
                this.data.Books.Remove(bookData);
                this.data.SaveChanges();
            }

            return isValid;
        }

        public async Task<int> Edit(int id, string Title, IList<IFormFile> bookPhoto, string? Language, int GenresId, int Pages, bool IsRecomended, string authorFName, string authorLName, string publisherName, string description)
        {
            Book? book = await this.data.Books.FindAsync(id);

            if (book == null)
            {
                return 0;
            }
            byte[] photo = new byte[8000];
            foreach (var item in bookPhoto)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        photo = stream.ToArray();
                    }
                }
            }
            int authorId = 0;
            int publisherId = 0;

            if (authorFName is not null && authorLName is not null)
            {
                authorId = authorService.Create(authorFName, authorLName);
            }

            if (publisherName is not null)
            {
                publisherId = publisherService.Create(publisherName);
            }

            if (bookPhoto.Count <= 0)
            {
                return 0;
            }

            if (authorId is 0 || publisherId is 0)
            {
                return 0;
            }

            if (bookPhoto.Count <= 0)
            {
                return 0;
            }

            book.Title = Title;
            book.BookPhoto = photo;
            book.Language = Language;
            book.GenresId = GenresId;
            book.Pages = Pages;
            book.IsRecommended = IsRecomended;
            book.Description = description;

            await this.data.SaveChangesAsync();

            return book.Id;
        }

        public async Task<int> Create(string Title, string AuthorFirstName, string AuthorLastName, string PublisherName, IList<IFormFile> bookPhoto, string? Language, int GenresId, int Pages, bool IsRecomended, string userId, string description)
        {
            byte[] photo = new byte[8000];
            foreach (var item in bookPhoto)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        photo = stream.ToArray();
                    }
                }
            }
            int authorId = 0;
            int publisherId = 0;

            if (AuthorFirstName is not null && AuthorLastName is not null)
            {
                authorId = authorService.Create(AuthorFirstName, AuthorLastName);
            }

            if (PublisherName is not null)
            {
                publisherId = publisherService.Create(PublisherName);
            }

            if (bookPhoto.Count <= 0)
            {
                return 0;
            }

            if (authorId is 0 || publisherId is 0)
            {
                return 0;
            }

            Book bookData = new Book
            {
                Title = Title,
                AuthorId = authorId,
                BookPhoto = photo,
                GenresId = GenresId,
                PublisherId = publisherId,
                Language = Language,
                Pages = Pages,
                IsRecommended = IsRecomended,
                UserId = userId,
                Description = description
            };

            await this.data.Books.AddAsync(bookData);
            await this.data.SaveChangesAsync();

            return bookData.Id;
        }

        public BookQueryModel All(string title, string searchTerm, BookSorting sorting, int currentPage, int booksPerPage)
        {
            var booksQuery = this.data.Books
                                            .Where(p => p.Language != null || p.Language == null);

            if (!string.IsNullOrWhiteSpace(title))
            {
                booksQuery = booksQuery.Where(c => c.Title == title);
            }

            booksQuery = sorting switch
            {
                BookSorting.Pages => booksQuery.OrderByDescending(c => c.Pages),
                BookSorting.Recomended => booksQuery.Where(c => c.IsRecommended == true),
                BookSorting.Title or _ => booksQuery.OrderByDescending(c => c.Title)
            };

            int totalBooks = booksQuery.Count();

            if (searchTerm != null)
            {
                booksQuery = booksQuery.Where(c => c.Title.Contains(searchTerm) || c.Author.FirstName.Contains(searchTerm) || c.Author.LastName.Contains(searchTerm));
            }

            var books = GetBooks(booksQuery
                 .Skip((currentPage - 1) * booksPerPage)
                 .Take(booksPerPage)
                 .AsQueryable());

            return new BookQueryModel
            {
                TotalBooks = totalBooks,
                CurrentPage = currentPage,
                BooksPerPage = booksPerPage,
                Books = books
            };
        }
    }
}
