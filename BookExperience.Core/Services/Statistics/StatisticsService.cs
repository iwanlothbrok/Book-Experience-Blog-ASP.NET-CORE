namespace BookExperience.Core.Services.Statistics
{
    using BookExperience.Core.Models.Statistics;
    using BookExperience.Infrastrucutre.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext data;
        public StatisticsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public StatisticsServiceModel Total(string userId)
        {
            StatisticsServiceModel model = new StatisticsServiceModel();


            List<int?> pages = this.data.Books.Where(c => c.UserId == userId).Select(c => c.Pages).ToList();
            int booksCount = this.data.Books.Where(c => c.UserId == userId).ToList().Count();
            var genresCount = this.data.Books.Where(c => c.UserId == userId).Select(c => c.GenresId).ToHashSet(); // this maybe wont work

            int? pagesCount = 0;
            foreach (var bookPage in pages)
            {
                pagesCount += bookPage;
            }

            model.TotalGenresReaded = genresCount.Count;
            model.TotalBooksReaded = booksCount;
            model.TotalPagesReaded = pagesCount;

            return model;
        }
    }
}
