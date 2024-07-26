using Application.Common.Interfaces;
using Application.Repositories;
using Application.Services;
using Infrastructure.Authentication;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetDbConnectionStringBuilder().ConnectionString;
        return serviceCollection
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, action => action.MigrationsAssembly("Infrastructure")))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IApprovalFlowRepository, ApprovalFlowRepository>()
            .AddScoped<ICashAdvanceRepository, CashAdvanceRepository>()
            .AddScoped<IGrantRepository, GrantRepository>()
            .AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>()
            .AddScoped<IRequisitionRepository, RequisitionRepository>()
            .AddScoped<ISubmitterRepository, SubmitterRepository>()
            .AddScoped<IExpenseHeadRepository, ExpenseHeadRepository>();
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IApprovalFlowService, ApprovalFlowService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICurrentUser, CurrentUser>();

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