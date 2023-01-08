namespace BookExperience.Core.Services.ApplicationUser
{
	using BookExperience.Infrastrucutre.Data.Models;

	public interface IApplicationUserService
	{
		ApplicationUser? FindApplicationUserById(string id); 
	}
}
