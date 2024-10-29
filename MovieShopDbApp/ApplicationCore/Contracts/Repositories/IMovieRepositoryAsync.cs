using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepositoryAsync : IRepositoryAsync<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int movieId);
        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreId);
        Task<IEnumerable<Movie>> GetTopPurchasedMoviesAsync();
        Task<IEnumerable<Movie>> GetHighestGrossingMovies(int count);
    }
}
