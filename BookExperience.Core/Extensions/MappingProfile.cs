namespace BookExperience.Core.Extensions
{
    using AutoMapper;
    using BookExperience.Core.Models;
    using BookExperience.Infrastrucutre.Data.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //mapping for books
            this.CreateMap<Book, MineBooksModel>();
        }
    }
}
