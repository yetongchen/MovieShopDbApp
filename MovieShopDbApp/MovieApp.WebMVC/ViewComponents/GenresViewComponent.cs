using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Contracts.Repositories;

namespace MovieApp.WebMVC.ViewComponents
{
    public class GenresViewComponent : ViewComponent
    {
        private readonly IGenreRepositoryAsync _genreRepository;

        public GenresViewComponent(IGenreRepositoryAsync genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return View(genres); 
        }
    }
}
