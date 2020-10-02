using API.Helpers;
using AutoMapper;
using Core.Interfaces;
using Core.Services;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Configuration.Services
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddCors();

            services.AddDbContext<DatabaseContext>(
                ob => ob.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" }));
        }
    }
}