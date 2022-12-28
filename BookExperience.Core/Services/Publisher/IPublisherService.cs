namespace BookExperience.Core.Services.Publisher
{
	public interface IPublisherService
	{
		int Create(string name);
        int DidPublisherExists(string name);
    }
}
