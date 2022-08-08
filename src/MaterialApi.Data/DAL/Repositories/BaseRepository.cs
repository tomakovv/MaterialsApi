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

        public IQueryable<T> GetAllAsync() => _context.Set<T>().AsNoTracking();

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression).AsNoTracking();

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}