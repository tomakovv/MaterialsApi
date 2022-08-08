using MaterialsApi.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MaterialsApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
           => services.AddDbContext<MaterialsContext>(options => options.UseSqlServer(configuration["ConnectionString"]));
    }
}