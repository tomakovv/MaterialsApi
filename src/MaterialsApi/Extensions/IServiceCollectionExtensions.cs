using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.DAL.Repositories;
using MaterialsApi.Services;
using MaterialsApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MaterialsApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
           => services.AddDbContext<MaterialsContext>(options => options.UseSqlServer(configuration["ConnectionString"]));

        public static void AddCustomLogger(this ILoggingBuilder logging, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
              .ReadFrom.Configuration(configuration)
               .Enrich.FromLogContext()
               .CreateLogger();
            logging.ClearProviders();
            logging.AddSerilog(logger);
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IMaterialsRepository, MaterialsRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IMaterialTypesRepository, MaterialTypesRepository>();
            services.AddScoped<IReviewsRepository, ReviewsRepository>();
            services.AddScoped<IMeterialsService, MaterialsService>();
            services.AddScoped<IAuthorsService, AuthorsService>();
            services.AddScoped<IMaterialTypesService, MaterialTypesService>();
            services.AddScoped<IReviewService, ReviewService>();
        }

        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(p => p.AddPolicy("default", builder =>
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            ));
        }
    }
}