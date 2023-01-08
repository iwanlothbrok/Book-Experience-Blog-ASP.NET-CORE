namespace BookExperience.Core.Services.Author
{
    using BookExperience.Infrastrucutre.Data.Models;

    public interface IAuthorService
    {
        Author? GetAuthorInfo(int id);
        int Create(string firstName, string lastName);
        int DidAuthorExists(string firstName, string lastName);
    }
}
