
using Common.DataInitializer;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class ServiceExtensions
{
    public static void AddCommon(this IServiceCollection services)
    {
        //services.AddScoped<IDataInitializer, IDataInitializer>();
    }
}