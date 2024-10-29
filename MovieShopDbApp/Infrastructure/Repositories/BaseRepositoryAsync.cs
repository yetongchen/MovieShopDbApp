using ApplicationCore.Contracts.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Repositories
{
    public class BaseRepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly MovieShopDbContext _context;
        public BaseRepositoryAsync(MovieShopDbContext context)
        {
            _context = context;
        }
        public async Task<int> DeleteAsync(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result != null)
            {
                _context.Set<T>().Remove(result);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> GetCount(Expression<Func<T, bool>> filter)
        {
            if (filter != null)
            {
                return await _context.Set<T>().Where(filter).CountAsync();
            }
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
