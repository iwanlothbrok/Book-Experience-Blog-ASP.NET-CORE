namespace BookExperience.Core.Extensions
{
    using AutoMapper;
    using BookExperience.Core.Models.Books;
    using BookExperience.Infrastrucutre.Data.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //mapping for books
            this.CreateMap<Book, MineBooksModel>();
            this.CreateMap<Book, BookFormModel>();
            this.CreateMap<Book, BookDetailsModel>();
            this.CreateMap<BookDetailsModel, BookFormModel>();
        }
    }
}
