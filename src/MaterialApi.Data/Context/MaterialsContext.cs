using MaterialsApi.Data.Entities;
using MaterialsApi.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaterialsApi.Data.Context
{
    public class MaterialsContext : IdentityDbContext<User>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public MaterialsContext(DbContextOptions<MaterialsContext> options) : base(options)
        {
        }
    }
}