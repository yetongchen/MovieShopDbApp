using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieApp.WebMVC.Controllers
{
    public class CastController : BaseController
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService, IGenreService genreService) : base(genreService)
        {
            _castService = castService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var castDetails = await _castService.GetCastDetails(id);
            if (castDetails == null) return NotFound();

            return View(castDetails);
        }
    }
}
