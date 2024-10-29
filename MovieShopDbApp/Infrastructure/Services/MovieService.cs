using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepositoryAsync _movieRepository;
        private readonly IPurchaseRepositoryAsync _purchaseRepository;

        public MovieService(IMovieRepositoryAsync movieRepository, IPurchaseRepositoryAsync purchaseRepository)
        {
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
        }

        public async Task<PaginationModel<MovieCardModel>> GetMoviesForHomePageAsync(int page, int pageSize)
        {
            var moviesQuery = await _movieRepository.GetAllAsync();
            var totalMovies = moviesQuery.Count();

            var movies = moviesQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MovieCardModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl
                })
                .ToList();
            return new PaginationModel<MovieCardModel>(movies, totalMovies, page, pageSize);
        }

        public async Task<PaginationModel<MovieCardModel>> GetMoviesByGenreAsync(int genreId, int page, int pageSize)
        {
            var moviesQuery = await _movieRepository.GetMoviesByGenreAsync(genreId);
            var totalMovies = moviesQuery.Count();
            
            var movies = moviesQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MovieCardModel
                { 
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl
                }).ToList();
            return new PaginationModel<MovieCardModel>(movies, totalMovies, page, pageSize);
        }

        public async Task<PaginationModel<MovieCardModel>> GetMoviesPurchasedByUserIdAsync(int userId, int page, int pageSize)
        {
            var purchasedMoviesQuery = await _purchaseRepository.GetMoviesPurchasedByUserIdAsync(userId);
            var totalMovies = purchasedMoviesQuery.Count();

            var purchasedMovies = purchasedMoviesQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MovieCardModel 
                { 
                    Id = m.Id, 
                    Title = m.Title, 
                    PosterUrl = m.PosterUrl 
                }).ToList();
            return new PaginationModel<MovieCardModel>(purchasedMovies, totalMovies, page, pageSize);
        }

        public async Task<MovieDetailModel> GetMovieDetailAsync(int movieId, int userId = 0)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(movieId);
            if (movie == null) return null;

            bool isPurchased = userId > 0 && await _purchaseRepository.IsMoviePurchasedByUserAsync(movieId, userId);

            return new MovieDetailModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Runtime = movie.Runtime,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                ReleaseDate = movie.ReleaseDate,
                Genres = movie.MovieGenres.Select(g => g.Genre.Name).ToList(),
                Casts = movie.MovieCasts.Select(c => new CastModel { Id = c.Cast.Id, Name = c.Cast.Name, Character = c.Character, ProfilePath = c.Cast.ProfilePath }).ToList(),
                Trailers = movie.Trailers.Select(t => new TrailerModel { Id = t.Id, Name = t.Name, TrailerUrl = t.TrailerUrl }).ToList(),
                IsPurchased = isPurchased
            };
        }
    }
}
