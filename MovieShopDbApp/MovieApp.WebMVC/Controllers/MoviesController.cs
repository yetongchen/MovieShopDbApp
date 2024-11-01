using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieApp.WebMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreRepositoryAsync _genreRepository;

        public MoviesController(IMovieService movieService, IGenreRepositoryAsync genreRepository)
        {
            _movieService = movieService;
            _genreRepository = genreRepository;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 30)
        {
            var movies = await _movieService.GetMoviesForHomePageAsync(page, pageSize);
            ViewBag.Action = "Index";
            ViewBag.Controller = "Movies";
            return View(movies);
        }

        public async Task<IActionResult> MoviesByGenre(int genreId, int page = 1, int pageSize = 30)
        {
            var movies = await _movieService.GetMoviesByGenreAsync(genreId, page, pageSize);
            var genre = await _genreRepository.GetByIdAsync(genreId);
            if (genre == null)
            {
                return NotFound();
            }
            ViewBag.Action = "MoviesByGenre";
            ViewBag.Controller = "Movies";
            ViewBag.GenreName = genre.Name;
            ViewBag.GenreId = genreId;
            return View(movies);
        }

        public async Task<IActionResult> Details(int id, int userId = 0)
        {
            var movieDetail = await _movieService.GetMovieDetailAsync(id, userId);
            if (movieDetail == null)
                return NotFound();

            return View(movieDetail);
        }
    }
}
