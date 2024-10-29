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
        Task<PaginationModel<MovieCardModel>> GetMoviesForHomePageAsync(int page, int pageSize);
        Task<PaginationModel<MovieCardModel>> GetMoviesByGenreAsync(int genreId, int page, int pageSize);
        Task<PaginationModel<MovieCardModel>> GetMoviesPurchasedByUserIdAsync(int userId, int page, int pageSize);
        Task<MovieDetailModel> GetMovieDetailAsync(int movieId, int userId);
    }
}
