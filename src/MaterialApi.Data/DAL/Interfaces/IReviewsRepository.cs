using MaterialsApi.Data.Entities;
using System.Linq.Expressions;

namespace MaterialsApi.Data.DAL.Interfaces
{
    public interface IReviewsRepository : IBaseRepository<Review>
    {
        Task<IEnumerable<Review>> GetAllWithMembersAsync();

        Task<Review> GetByConditionWithMembersAsync(Expression<Func<Review, bool>> expression);
    }
}