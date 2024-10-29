using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IPurchaseRepositoryAsync : IRepositoryAsync<Purchase>
    {
        Task<bool> IsMoviePurchasedByUserAsync(int movieId, int userId);
        Task<IEnumerable<Movie>> GetMoviesPurchasedByUserIdAsync(int userId);
        Task<int> AddPurchaseAsync(Purchase purchase);
        Task<int> GetPurchaseCountForMovieAsync(int movieId);
        Task<IEnumerable<MoviePurchaseReportModel>> GetTopPurchasedMoviesReportAsync(DateTime? fromDate, DateTime? toDate, int page, int pageSize);
    }
}
