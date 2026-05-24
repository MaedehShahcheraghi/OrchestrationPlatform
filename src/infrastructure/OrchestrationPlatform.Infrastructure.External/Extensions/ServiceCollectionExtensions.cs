using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using OrchestrationPlatform.Application.Abstractions.Services.External;
using OrchestrationPlatform.Infrastructure.External.Services;

namespace OrchestrationPlatform.Infrastructure.External.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMinio(configureClient =>
        {
            configureClient
                .WithEndpoint(configuration["MinIO:Endpoint"])
                .WithCredentials(configuration["MinIO:AccessKey"], configuration["MinIO:SecretKey"])
                .WithSSL(configuration.GetValue<bool>("MinIO:UseSSL"))
                .Build();
        });
        services.AddScoped<IObjectStorageService, MinIoObjectStorageService>();

        services.AddHttpClient<IOrchestrationService, N8NOrchestrationService>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }
}