using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepository:IRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int movieId);
        Task<IEnumerable<Movie>> GetAllMoviesAsync();

        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreId);
        Task<IEnumerable<Movie>> GetTopPurchasedMoviesAsync();
        Task<int> AddMovieAsync(Movie movie);
    }
}
