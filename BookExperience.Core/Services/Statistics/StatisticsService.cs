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

        public StatisticsServiceModel Total()
        {
            StatisticsServiceModel model = new StatisticsServiceModel();


            List<int?> pages = this.data.Books.Select(c => c.Pages).ToList();
            int booksCount = this.data.Books.ToList().Count();
            var genresCount = this.data.Books.Select(c => c.GenresId).ToHashSet(); // this maybe wont work

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
