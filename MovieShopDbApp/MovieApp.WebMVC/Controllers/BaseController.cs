using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MovieApp.WebMVC.Controllers
{
    public class BaseController : Controller
    {
        private readonly IGenreService _genreService;

        public BaseController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var genres = await _genreService.GetAllGenresAsync();
            ViewBag.Genres = genres;

            await next();
        }
    }
}
