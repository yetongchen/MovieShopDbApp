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

        // 显示首页的电影列表
        public async Task<IActionResult> Index(int page = 1, int pageSize = 20)
        {
            var movies = await _movieService.GetMoviesForHomePageAsync(page, pageSize);
            return View(movies);
        }

        // 按类型筛选电影
        public async Task<IActionResult> ByGenre(int genreId, int page = 1, int pageSize = 20)
        {
            var movies = await _movieService.GetMoviesByGenreAsync(genreId, page, pageSize);
            var genre = _genreService.GetGenreByIdAsync(genreId);
            if (genre == null)
            {
                return NotFound();
            }
            ViewBag.GenreName = genre.Name;
            ViewBag.GenreId = genreId;
            return View(movies);
        }

        // 显示电影详情
        public async Task<IActionResult> Details(int id, int userId = 0)
        {
            var movieDetail = await _movieService.GetMovieDetailAsync(id, userId);
            if (movieDetail == null)
                return NotFound();

            return View(movieDetail);
        }
    }
}
