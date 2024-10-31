using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using MovieApp.WebMVC.Utility.Filters;

namespace MovieApp.WebMVC.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService, IGenreService genreService) : base(genreService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> TopMovies(DateTime? fromDate, DateTime? toDate, int page = 1, int pageSize = 20)
        {
            var report = await _adminService.GetTopPurchasedMoviesReportAsync(fromDate, toDate, page, pageSize);
            return View(report);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        [ServiceFilter(typeof(LogFilter))]
        public async Task<IActionResult> AddMovie(MovieCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var newMovieId = await _adminService.AddMovieAsync(model);
                if (newMovieId > 0)
                    return RedirectToAction("TopMovies");
            }

            return View(model);
        }
    }
}
