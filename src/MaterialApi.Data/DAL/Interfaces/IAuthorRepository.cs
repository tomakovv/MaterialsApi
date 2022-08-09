using MaterialsApi.Data.Entities;
using System.Linq.Expressions;

namespace MaterialsApi.Data.DAL.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllWithMembersAsync();

        Task<Author> GetSingleByParameterWithMembersAsync(Expression<Func<Author, bool>> expression);
    }
}