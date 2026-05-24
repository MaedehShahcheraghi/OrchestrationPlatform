using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OrchestrationPlatform.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        return services;
    }
}