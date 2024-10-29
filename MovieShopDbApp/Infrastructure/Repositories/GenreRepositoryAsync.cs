using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenreRepositoryAsync : BaseRepositoryAsync<Genre>, IGenreRepositoryAsync
    {
        private readonly MovieShopDbContext _dbContext;

        public GenreRepositoryAsync(MovieShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
