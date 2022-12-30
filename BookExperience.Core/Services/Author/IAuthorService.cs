namespace BookExperience.Core.Services.Author
{
    public interface IAuthorService
    {
        int Create(string firstName, string lastName);
        int DidAuthorExists(string firstName, string lastName);
    }
}
