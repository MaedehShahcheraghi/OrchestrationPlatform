namespace OrchestrationPlatform.Application.Abstractions.Services.SeedData;

public interface IApplicationDbSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}