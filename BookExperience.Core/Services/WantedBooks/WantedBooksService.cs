namespace BookExperience.Core.Services.WantedBooks
{
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using System.Collections.Generic;
    public class WantedBooksService : IWantedBooksService
    {
        private readonly ApplicationDbContext data;

        public WantedBooksService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public List<WantedBook> GetWantedBooksByUserId(string id)
        => this.data.WantedBooks.Where(C => C.ApplicationUserId == id).ToList();

        public bool AddWantedBooks(int bookId, string userId)
        {
            if (bookId == 0 || userId == null)
            {
                return false;
            }
            WantedBook wantedBook = new WantedBook()
            {
                ApplicationUserId = userId,
                BookId = bookId
            };

            this.data.WantedBooks.Add(wantedBook);
            this.data.SaveChanges();

            return true;
        }

        public bool DoesBookIsWantedByUser(int bookId, string userId)
        {
            var allWantedBooks = this.data.WantedBooks.ToList();

            foreach (var book in allWantedBooks)
            {
                if (book.ApplicationUserId == userId && book.BookId == bookId)
                {
                    return true;
                }
            }

            return false;
        }

        public bool RemoveWantedBook(int id)
        {
            var info = this.data.WantedBooks.Find(id);

            if (info != null)
            {
                this.data.WantedBooks.Remove(info);
                this.data.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
