using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrchestrationPlatform.Application.Abstractions.Persistence;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Infrastructure.Persistence.Contexts;
using OrchestrationPlatform.Infrastructure.Persistence.Repositories;
using OrchestrationPlatform.Infrastructure.Persistence.Repositories.Common;

namespace OrchestrationPlatform.Infrastructure.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");

        services.AddDbContextPool<OrchestrationWriteDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(OrchestrationWriteDbContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure();
            });
        });

        services.AddDbContextPool<OrchestrationReadDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions => { sqlOptions.EnableRetryOnFailure(); });

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });


        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IRepository<>), typeof(WriteRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(ReadRepository<>));

        return services;
    }
}