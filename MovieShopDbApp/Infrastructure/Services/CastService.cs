using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailModel> GetCastDetails(int castId)
        {
            var cast = await _castRepository.GetCastWithMoviesAsync(castId);
            if (cast == null) return null;

            var castDetail = new CastDetailModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                TmdbUrl = cast.TmdbUrl,
                ProfilePath = cast.ProfilePath,
                Movies = cast.MovieCasts.Select(mc => new MovieCardModel
                {
                    Id = mc.MovieId,
                    Title = mc.Movie.Title,
                    PosterUrl = mc.Movie.PosterUrl
                }).ToList()
            };
            return castDetail;
        }
    }
}
