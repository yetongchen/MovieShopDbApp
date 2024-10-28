using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        Task<PaginatedList<MovieCardModel>> GetMoviesForHomePageAsync(int page, int pageSize);
        Task<PaginatedList<MovieCardModel>> GetMoviesByGenreAsync(int genreId, int page, int pageSize);
        Task<PaginatedList<MovieCardModel>> GetMoviesPurchasedByUserIdAsync(int userId, int page, int pageSize);
        Task<MovieDetailModel> GetMovieDetailAsync(int movieId, int userId);
    }
}
