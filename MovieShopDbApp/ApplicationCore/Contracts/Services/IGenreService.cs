using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IGenreService
    {
        Task<GenreModel> GetGenreByIdAsync(int genreId);
        Task<IEnumerable<GenreModel>> GetAllGenresAsync();
    }
}
