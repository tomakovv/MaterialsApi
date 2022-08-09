using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MaterialsApi.Data.DAL.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private readonly MaterialsContext _context;

        public AuthorRepository(MaterialsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Author> GetSingleByParameterWithMembersAsync(Expression<Func<Author, bool>> expression) => await _context.Authors.
            Include(m => m.CreatedMaterials).
            ThenInclude(m => m.Reviews).
            AsNoTracking().
            SingleOrDefaultAsync(expression);

        public async Task<IEnumerable<Author>> GetAllWithMembersAsync() =>
            await _context.Authors.
            Include(m => m.CreatedMaterials).
            ThenInclude(m => m.Reviews).
            AsNoTracking().ToListAsync();
    }
}