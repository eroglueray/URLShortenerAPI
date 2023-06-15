using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using URLShortenerAPI.Context;
using URLShortenerAPI.Data.DataManager;
using URLShortenerAPI.Data.DataManager.Interfaces;
using URLShortenerAPI.Data.Entities;
using URLShortenerAPI.Data.UnitOfWork;
using URLShortenerAPI.Repositories;

namespace URLShortenerAPI.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddDbContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApiDbContext>(options => options.UseSqlite(connStr));
            services.AddTransient<IURLShortenerUnitOfWork, URLShortenerUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IURLDataManager, URLDataManager>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
