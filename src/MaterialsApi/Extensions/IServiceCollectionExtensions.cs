using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.DAL.Repositories;
using MaterialsApi.Services;
using MaterialsApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MaterialsApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
           => services.AddDbContext<MaterialsContext>(options => options.UseSqlServer(configuration["ConnectionString"]));

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IMaterialsRepository, MaterialsRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IMaterialTypesRepository, MaterialTypesRepository>();
            services.AddScoped<IReviewsRepository, ReviewsRepository>();
            services.AddScoped<IMeterialsService, MeterialsService>();
        }
    }
}