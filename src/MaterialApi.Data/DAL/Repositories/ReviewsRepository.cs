using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MaterialsApi.Data.DAL.Repositories
{
    public class ReviewsRepository : BaseRepository<Review>, IReviewsRepository
    {
        private readonly MaterialsContext _context;

        public ReviewsRepository(MaterialsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllWithMembersAsync() => await _context.Reviews.Include(r => r.Material).AsNoTracking().ToListAsync();

        public async Task<Review> GetByConditionWithMembersAsync(Expression<Func<Review, bool>> expression) =>
           await _context.Reviews.Include(r => r.Material).SingleOrDefaultAsync(expression);
    }
}