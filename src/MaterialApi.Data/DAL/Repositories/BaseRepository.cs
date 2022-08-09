using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MaterialsApi.Data.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly MaterialsContext _context;

        public BaseRepository(MaterialsContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll() => _context.Set<T>().AsNoTracking();

        public async Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression) => await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(expression);

        public async Task<T> CreateAsync(T entity)
        {
            var addedElement = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return addedElement.Entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}