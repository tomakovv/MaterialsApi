using MaterialsApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaterialsApi.Data.Context
{
    public class MaterialsContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public MaterialsContext(DbContextOptions options) : base(options)
        {
        }
    }
}