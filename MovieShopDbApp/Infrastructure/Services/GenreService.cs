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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepositoryAsync _genreRepository;

        public GenreService(IGenreRepositoryAsync genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreModel> GetGenreByIdAsync(int genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);
            if (genre == null)
            {
                return null;
            }
            return new GenreModel { Id = genre.Id, Name = genre.Name };
        }

        public async Task<IEnumerable<GenreModel>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            if (!genres.Any())
            {
                Console.WriteLine("No genres found in the database.");
            }
            return genres.Select(g => new GenreModel { Id = g.Id, Name = g.Name });
        }
    }
}
