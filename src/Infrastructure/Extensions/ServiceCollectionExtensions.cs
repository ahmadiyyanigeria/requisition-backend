using Application.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetDbConnectionStringBuilder().ConnectionString;
        return serviceCollection
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, action => action.MigrationsAssembly("Infrastructure")))
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection;

    }

    public static IServiceCollection AddEmailService(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddCacheService(this IServiceCollection services, string connectionString)
    {
        return services.AddMemoryCache();
    }
}