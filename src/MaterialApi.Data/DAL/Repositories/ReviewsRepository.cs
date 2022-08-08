using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.Entities;

namespace MaterialsApi.Data.DAL.Repositories
{
    public class ReviewsRepository : BaseRepository<Review>, IReviewsRepository
    {
        private readonly MaterialsContext _context;

        public ReviewsRepository(MaterialsContext context) : base(context)
        {
            _context = context;
        }
    }
}