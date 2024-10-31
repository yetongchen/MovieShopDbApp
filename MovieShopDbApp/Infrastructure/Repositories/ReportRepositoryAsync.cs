using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReportRepositoryAsync : IReportRepositoryAsync
    {
        private readonly MovieShopDbContext _dbContext;

        public ReportRepositoryAsync(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MoviePurchaseReportModel>> GetTopPurchasedMoviesReportAsync(DateTime? fromDate, DateTime? toDate, int page, int pageSize)
        {
            var query = _dbContext.Purchases.AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(p => p.PurchaseDateTime >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(p => p.PurchaseDateTime <= toDate.Value);
            }

            var moviePurchases = await query
                .GroupBy(p => p.MovieId)
                .OrderByDescending(g => g.Count())
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(g => new MoviePurchaseReportModel
                {
                    MovieId = g.Key,
                    Title = g.Select(p => p.Movie.Title).FirstOrDefault(),
                    TotalPurchases = g.Count()
                })
                .ToListAsync();

            return moviePurchases;
        }
    }
}
