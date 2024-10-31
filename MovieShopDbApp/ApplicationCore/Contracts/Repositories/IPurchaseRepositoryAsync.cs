using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IPurchaseRepositoryAsync : IRepositoryAsync<Purchase>
    {
        Task<bool> IsMoviePurchasedByUserAsync(int movieId, int userId);
        Task<IEnumerable<Movie>> GetMoviesPurchasedByUserIdAsync(int userId);
        Task<int> AddPurchaseAsync(Purchase purchase);
        Task<int> GetPurchaseCountForMovieAsync(int movieId);
    }
}
