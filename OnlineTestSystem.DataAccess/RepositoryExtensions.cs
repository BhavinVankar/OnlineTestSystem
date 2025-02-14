using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineTestSystem.DataAccess.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestSystem.DataAccess
{
    public static class RepositoryExtensions
    {
        public static void AddRepositoryLayer(this IServiceCollection services,IConfiguration configuration)
        {
            // INFO: Below lines will register all the repositories
            typeof(IUserRepository).Assembly.GetTypes()
                .Where(s => s.Name.EndsWith("Repository") && !s.IsInterface
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
