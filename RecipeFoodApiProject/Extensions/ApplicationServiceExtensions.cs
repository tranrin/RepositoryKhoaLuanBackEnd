
using Application;
using Application.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFoodApiProject.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            //});

            //services.AddDbContext<DataContext>(opt =>
            //{
            //    opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            //});

            //services.AddMediatR(typeof(Details.Handler).Assembly);
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddApplication();

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}
