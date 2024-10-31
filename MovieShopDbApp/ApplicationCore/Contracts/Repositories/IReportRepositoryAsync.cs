using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IReportRepositoryAsync
    {
        Task<IEnumerable<MoviePurchaseReportModel>> GetTopPurchasedMoviesReportAsync(DateTime? fromDate, DateTime? toDate, int page, int pageSize);
    }
}
