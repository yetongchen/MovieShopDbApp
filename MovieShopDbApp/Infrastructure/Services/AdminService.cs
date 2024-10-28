using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieRepository _movieRepository;

        public AdminService(IPurchaseRepository purchaseRepository, IMovieRepository movieRepository)
        {
            _purchaseRepository = purchaseRepository;
            _movieRepository = movieRepository;
        }

        public async Task<int> AddMovieAsync(MovieCreateModel model)
        {
            var movie = new Movie
            {
                Title = model.Title,
                Overview = model.Overview,
                Tagline = model.Tagline,
                Runtime = model.Runtime,
                Budget = model.Budget,
                Revenue = model.Revenue,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                ImdbUrl = model.ImdbUrl,
                TmdbUrl = model.TmdbUrl,
                ReleaseDate = model.ReleaseDate,
                OriginalLanguage = model.OriginalLanguage
            };

            int newMovieId = await _movieRepository.AddMovieAsync(movie);
            return newMovieId;
        }

        public async Task<IEnumerable<MoviePurchaseReportModel>> GetTopPurchasedMoviesReportAsync(DateTime? fromDate, DateTime? toDate, int page, int pageSize)
        {
            return await _purchaseRepository.GetTopPurchasedMoviesReportAsync(fromDate, toDate, page, pageSize);
        }
    }
}
