﻿using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
    }
}
