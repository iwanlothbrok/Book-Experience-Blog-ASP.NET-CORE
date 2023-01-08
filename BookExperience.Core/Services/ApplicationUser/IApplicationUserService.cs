namespace BookExperience.Core.Services.ApplicationUser
{
	using BookExperience.Infrastrucutre.Data.Models;

	public interface IApplicationUserService
	{
		List<Book>? GetUserWantedBooks(string id);
		ApplicationUser? FindApplicationUserById(string id);

    }
}
