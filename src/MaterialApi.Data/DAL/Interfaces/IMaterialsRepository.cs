using MaterialsApi.Data.Entities;
using System.Linq.Expressions;

namespace MaterialsApi.Data.DAL.Interfaces
{
    public interface IMaterialsRepository : IBaseRepository<Material>
    {
        Task<IEnumerable<Material>> GetAllWithMembersAsync();

        Task<Material> GetByConditionWithMembersAsync(Expression<Func<Material, bool>> expression);
    }
}