using MaterialsApi.Data.Context;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.DAL.Repositories;
using MaterialsApi.Data.Entities.Identity;
using MaterialsApi.Services;
using MaterialsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            services.AddScoped<IMeterialsService, MaterialsService>();
            services.AddScoped<IAuthorsService, AuthorsService>();
            services.AddScoped<IMaterialTypesService, MaterialTypesService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<MaterialsContext>()
               .AddDefaultTokenProviders();
        }

        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(p => p.AddPolicy("default", builder =>
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            ));
        }

        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(option =>
             {
                 option.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                 };
             });
        }

        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Bearer Authorization",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string [] {}
                }
            });
            });
        }
    }
}