using System.Linq.Expressions;

namespace MaterialsApi.Data.DAL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);

        Task DeleteAsync(T entity);

        IQueryable<T> GetAllAsync();

        Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression);

        Task UpdateAsync(T entity);
    }
}