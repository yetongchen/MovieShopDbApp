using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<MoviePurchaseReportModel>> GetTopPurchasedMoviesReportAsync(DateTime? fromDate, DateTime? toDate, int page, int pageSize);
        Task<int> AddMovieAsync(MovieCreateModel movie);
    }
}
