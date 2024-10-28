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
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public GenreModel GetGenreByIdAsync(int genreId)
        {
            var genre =_genreRepository.GetById(genreId);
            if (genre == null)
            {
                return null;
            }
            return new GenreModel { Id = genre.Id, Name = genre.Name };
        }

        public IEnumerable<GenreModel> GetAllGenresAsync()
        {
            var genres = _genreRepository.GetAll();
            if (!genres.Any())
            {
                Console.WriteLine("No genres found in the database.");
            }
            return genres.Select(g => new GenreModel { Id = g.Id, Name = g.Name });
        }
    }
}
