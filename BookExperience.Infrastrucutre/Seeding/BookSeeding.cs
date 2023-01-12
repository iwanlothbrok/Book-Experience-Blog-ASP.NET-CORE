namespace BookExperience.Infrastrucutre.Seeding
{
    using BookExperience.Infrastructure.Seeding;
    using BookExperience.Infrastrucutre.Data;
    using BookExperience.Infrastrucutre.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using static BookExperience.Infrastructure.Seeding.SeedingConstants;
    using static BookExperience.Infrastrucutre.Data.DataConstants;

    public class BookSeeding : ISeeder
    {
        public void Seed(ApplicationDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Books.Any(d => d.Id == 77))
            {
                string user1Id = GetUserId(userOneEmail, data);

                SeedBooks(data,
                    id: 77,
                    title: "Harry Potter and the Philosopher's Stone",
                    authordId: 77,
                    publisherId: 1,
                    userId: user1Id,
                    genresId: FantasyId,
                    language: "Bulgarian",
                    pages: 223,
                    isRecommended: true,
                    description: "Harry Potter, an eleven-year-old orphan," +
                    " discovers that he is a wizard and is invited to study at Hogwarts." +
                    " Even as he escapes a dreary life and enters a world of magic," +
                    " he finds trouble awaiting him.",
                    "wwwroot/img/harryPotter1.jpg");

                SeedBooks(data,
                    id: 78,
                    title: "Harry Potter and the Chamber of Secrets",
                    authordId: 77,
                    publisherId: 1,
                    userId: user1Id,
                    genresId: FantasyId,
                    language: "Bulgarian",
                    pages: 340,
                    isRecommended: false,
                    description: "Harry Potter and the Chamber of Secrets is a 1998 young adult fantasy novel by J.K. Rowling, the second in the Harry Potter series." +
                    " The story follows Harry's tumultuous second year at Hogwarts School" +
                    " of Witchcraft and Wizardry, including an encounter with Voldemort," +
                    " the wizard who killed Harry's parents.",
                    "wwwroot/img/harryPotter2.jpg");

                string userTwoId = GetUserId(userTwoEmail, data);

                SeedBooks(data,
                    id: 79,
                    title: "The Alchemist",
                    authordId: 78,
                    publisherId: 2,
                    userId: userTwoId,
                    genresId: PhilosophyId,
                    language: "English",
                    pages: 116,
                    isRecommended: true,
                    description: "The Alchemist is the magical story of Santiago," +
                    " an Andalusian shepherd boy who yearns to travel in search of a worldly treasure as extravagant as any ever found." +
                    " From his home in Spain he journeys to the markets of" +
                    " Tangiers and across the Egyptian desert to a fateful encounter with the alchemist.",
                    "wwwroot/img/theAlchemist.jpg");

                data.Database.OpenConnection();
                try
                {
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Books ON");
                    data.SaveChanges();
                    data.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Books OFF");
                }
                finally
                {
                    data.Database.CloseConnection();
                }
            }
        }

        private Book SeedBooks
            (ApplicationDbContext data,
            int id,
            string title,
            int authordId,
            int publisherId,
            string userId,
            int genresId,
            string language,
            int pages,
            bool isRecommended,
            string description,
            string bookPhotoPath)
        {
            Book book = new Book
            {
                Id = id,
                Title = title,
                AuthorId = authordId,
                PublisherId = publisherId,
                UserId = userId,
                GenresId = genresId,
                Language = language,
                Pages = pages,
                IsRecommended = isRecommended,
                Description = description,
                BookPhoto = ReadFile(bookPhotoPath)
            };

            data.Books.Add(book);

            return book;
        }
        public static byte[] ReadFile(string sPath)
        {
            byte[] data = null;

            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fStream);

            data = br.ReadBytes((int)numBytes);

            return data;
        }

        private string? GetUserId(string email, ApplicationDbContext data)
      => data
          .Users
          .Where(c => c.Email == email)
          .FirstOrDefault(u => u.Email == email).Id;
    }
}
