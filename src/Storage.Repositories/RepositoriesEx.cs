using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Storage.DAOs;
using Storage.Repositories.Interfaces;

namespace Storage.Repositories
{
    public static class RepositoriesEx
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database:ConnectionString")));
            services.AddTransient<IStoreRepository<StringDAO>, StringDAORepository>();

            return services;
        }
    }
}
