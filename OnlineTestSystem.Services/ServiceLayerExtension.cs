using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineTestSystem.DataAccess;
using OnlineTestSystem.Services.Abstraction;
using OnlineTestSystem.Services.AutoMapperConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.Services
{
    public static class ServiceLayerExtension
    {
        public static void AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddRepositoryLayer(configuration);

            var config = new MapperConfiguration(cfg =>
            {
                //Register Mapping Profile(AutoMapper Mapping Configuration)
                cfg.AddProfile<AutoMapperConfig>();
            });

            // ✅ Create an IMapper instance
            var mapper = config.CreateMapper();

            // ✅ Register the IMapper instance with DI
            services.AddSingleton(mapper);

            typeof(IUserHelper).Assembly.GetTypes()
                .Where(s => s.Name.EndsWith("Helper") && !s.IsInterface
                && !s.IsAbstract
                )
                .Select(s => new
                {
                    Type = s,
                    Interface = s.GetInterface($"I{s.Name}")
                }).ToList()
                .ForEach(s =>
                {
                    services.AddScoped(s.Interface, s.Type);
                });

        }
    }
}
