using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepositoryAsync : BaseRepositoryAsync<Purchase>, IPurchaseRepositoryAsync
    {
        private readonly MovieShopDbContext _dbContext;

        public PurchaseRepositoryAsync(MovieShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsMoviePurchasedByUserAsync(int movieId, int userId)
        {
            return await _dbContext.Purchases
                .AnyAsync(p => p.MovieId == movieId && p.UserId == userId);
        }

        public async Task<IEnumerable<Movie>> GetMoviesPurchasedByUserIdAsync(int userId)
        {
            return await _dbContext.Purchases
                .Where(p => p.UserId == userId)
                .Select(p => p.Movie)
                .ToListAsync();
        }

        public async Task<int> AddPurchaseAsync(Purchase purchase)
        {
            _dbContext.Purchases.Add(purchase);
            await _dbContext.SaveChangesAsync();
            return purchase.MovieId;
        }

        public async Task<int> GetPurchaseCountForMovieAsync(int movieId)
        {
            return await _dbContext.Purchases
                .CountAsync(p => p.MovieId == movieId);
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
