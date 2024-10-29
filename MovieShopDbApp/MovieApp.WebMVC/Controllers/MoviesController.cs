using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieApp.WebMVC.Controllers
{
    public class MoviesController : BaseController
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        public MoviesController(IMovieService movieService, IGenreService genreService) : base(genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 20)
        {
            var movies = await _movieService.GetMoviesForHomePageAsync(page, pageSize);
            ViewBag.Action = "Index";
            ViewBag.Controller = "Movies";
            return View(movies);
        }

        public async Task<IActionResult> MoviesByGenre(int genreId, int page = 1, int pageSize = 20)
        {
            var movies = await _movieService.GetMoviesByGenreAsync(genreId, page, pageSize);
            var genre = await _genreService.GetGenreByIdAsync(genreId);
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
