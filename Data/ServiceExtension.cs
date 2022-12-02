
using Data.Contracts;
using Data.DataInitializer;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class ServiceExtension
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        
        services.AddScoped<IDataInitializer, UserDataInitializer>();
        services.AddScoped<IDataInitializer, ProductDataInitializer>();
        services.AddScoped<IDataInitializer, ProductDetailDataInitializer>();
        services.AddScoped<IDataInitializer, StoresDataInitializer>();
        services.AddScoped<IDataInitializer, StoredProductsDataInitializer>();
    }
}
