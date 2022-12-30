namespace BookExperience.Core.Services.Statistics
{
    using BookExperience.Core.Models.Statistics;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

    public interface IStatisticsService
    {
        StatisticsServiceModel Total(string userId);
    }
}
