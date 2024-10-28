using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        private readonly MovieShopDbContext _dbContext;

        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await _dbContext.Movies
                .Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieCasts).ThenInclude(mc => mc.Cast)
                .Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == movieId);
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreId)
        {
            return await _dbContext.Movies
                .Where(m => m.MovieGenres.Any(g => g.GenreId == genreId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetTopPurchasedMoviesAsync()
        {
            var movieIds = await _dbContext.Purchases
                .GroupBy(p => p.MovieId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToListAsync();

            return await _dbContext.Movies
                .Where(m => movieIds.Contains(m.Id))
                .ToListAsync();
        }

        public async Task<int> AddMovieAsync(Movie movie)
        {
            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
            return movie.Id;
        }

        /*public IEnumerable<Movie> GetMoviesWithGenre()
        {
            return _context.Movies.Include(x => x.Genres).ToList();
        }

        public IEnumerable<Movie> GetTopRevenueMovies(int number = 20)
        {
            return _context.Movies.OrderByDescending(x=>x.Revenue).Take(number).Include(x=>x.Genres).ToList(); ;
        }*/
    }
}
