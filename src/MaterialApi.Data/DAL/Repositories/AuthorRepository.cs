using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.Entities;

namespace MaterialsApi.Data.DAL.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        private readonly MaterialsContext _context;

        public AuthorRepository(MaterialsContext context) : base(context)
        {
            _context = context;
        }
    }
}