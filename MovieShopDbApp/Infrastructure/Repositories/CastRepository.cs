using ApplicationCore.Contracts.Repositories;
using Infrastructure.Data;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CastRepository : BaseRepository<CastRepository>, ICastRepository
    {
        private readonly MovieShopDbContext _dbContext;

        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cast> GetCastWithMoviesAsync(int castId)
        {
            return await _dbContext.Casts
                .Include(c => c.MovieCasts)
                .ThenInclude(mc => mc.Movie)
                .FirstOrDefaultAsync(c => c.Id == castId);
        }

        public async Task<IEnumerable<Cast>> GetAllCastsAsync()
        {
            return await _dbContext.Casts.ToListAsync();
        }
    }
}
