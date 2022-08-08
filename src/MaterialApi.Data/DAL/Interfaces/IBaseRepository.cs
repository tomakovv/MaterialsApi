using System.Linq.Expressions;

namespace MaterialsApi.Data.DAL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task Create(T entity);
        Task Delete(T entity);
        IQueryable<T> GetAllAsync();
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        Task Update(T entity);
    }
}