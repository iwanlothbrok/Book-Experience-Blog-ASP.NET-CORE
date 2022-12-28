namespace BookExperience.Core.Services.Book
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookExperience.Core.Models;
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

        public IEnumerable<Genres> AllGenres()
            => this.data
                .Genres
                .Select(c => new Genres
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();


        public IEnumerable<MineBooksModel> ByUser(string userId)
       => GetBooks(this.data
           .Books
           .Where(c => c.UserId == userId))
           .ToList();

        public IEnumerable<MineBooksModel> GetBooks(IQueryable<Book> bookQuery)
      => bookQuery
          .ProjectTo<MineBooksModel>(this.mapper.ConfigurationProvider)
          .ToList();

        public async Task<int> Create(string Title, string AuthorFirstName, string AuthorLastName, string PublisherName, IList<IFormFile> bookPhoto, string Language, int GenresId, int Pages, bool IsRecomended, string userId)
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
            };

            await this.data.Books.AddAsync(bookData);
            await this.data.SaveChangesAsync();

            return bookData.Id;
        }
        public bool Delete(int id)
        {
            Book? bookData = this.data.Books.Find(id);

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
    }
}
