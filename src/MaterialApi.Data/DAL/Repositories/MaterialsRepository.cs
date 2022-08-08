using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MaterialsApi.Data.DAL.Repositories
{
    public class MaterialsRepository : BaseRepository<Material>, IMaterialsRepository
    {
        private readonly MaterialsContext _context;

        public MaterialsRepository(MaterialsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Material>> GetAllWithMembersAsync() => await _context.Materials
            .Include(m => m.Author)
            .Include(m => m.Reviews)
            .Include(m => m.Type).ToListAsync();

        public async Task<Material> GetByConditionWithMembersAsync(Expression<Func<Material, bool>> expression) => await _context.Materials
            .Include(m => m.Author)
            .Include(m => m.Reviews)
            .Include(m => m.Type).SingleOrDefaultAsync(expression);
    }
}