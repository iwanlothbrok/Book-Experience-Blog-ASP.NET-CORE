namespace BookExperience.Core.Services.ApplicationUser
{
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;

    public class ApplicationUserService : IApplicationUserService
    {
        private ApplicationDbContext data;
        public ApplicationUserService(ApplicationDbContext data)
        {
            this.data = data;
        }
        public ApplicationUser? FindApplicationUserById(string id)
        => this.data.ApplicationUsers.Find(id);

        public List<Book>? GetUserWantedBooks(string id)
        {
            var user = FindApplicationUserById(id);

            List<Book> books = user.WantedBooks.ToList();

            if (books == null)
            {
                return null;
            }
            return books;
        }
        public bool AddBook(Book book, ApplicationUser user)
        {
            bool userHasTheBook = UserHasThisBook(book, user);

            if (userHasTheBook == false)
            {
                user.WantedBooks.Add(book);
                this.data.SaveChanges();

                return true;
            }

            return false;
        }

        public bool UserHasThisBook(Book book, ApplicationUser user)
        {
            if (user.WantedBooks.Contains(book) == true)
            {
                return true;
            }
            return false;
        }
    }
}
