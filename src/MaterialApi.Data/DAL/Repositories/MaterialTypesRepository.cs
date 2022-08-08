using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.Entities;

namespace MaterialsApi.Data.DAL.Repositories
{
    public class MaterialTypesRepository : BaseRepository<MaterialType>, IMaterialTypesRepository
    {
        private readonly MaterialsContext _context;

        public MaterialTypesRepository(MaterialsContext context) : base(context)
        {
            _context = context;
        }
    }
}