using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface ICastRepository
    {
        Task<Cast> GetCastWithMoviesAsync(int castId);
        Task<IEnumerable<Cast>> GetAllCastsAsync();
    }
}
