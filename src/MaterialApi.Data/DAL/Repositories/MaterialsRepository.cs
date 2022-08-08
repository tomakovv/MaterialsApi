using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialsApi.Data.DAL.Repositories
{
    public class MaterialsRepository : BaseRepository<Material>, IMaterialsRepository
    {
        private readonly MaterialsContext _context;

        public MaterialsRepository(MaterialsContext context) : base(context)
        {
            _context = context;
        }
    }
}