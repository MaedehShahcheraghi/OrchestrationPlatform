using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrchestrationPlatform.Application.Abstractions.Persistence.Common;
using OrchestrationPlatform.Application.Abstractions.Services.SeedData;
using OrchestrationPlatform.Domain.Entities;
using OrchestrationPlatform.Domain.Enums;
using OrchestrationPlatform.Infrastructure.Persistence.Contexts;

namespace OrchestrationPlatform.Infrastructure.Persistence.SeedData;

public sealed class ApplicationDbSeeder(
    OrchestrationWriteDbContext context,
    IUnitOfWork unitOfWork,
    ILogger<ApplicationDbSeeder> logger)
    : IApplicationDbSeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (context.Database.IsRelational()) await context.Database.MigrateAsync(cancellationToken);

        await SeedSoftwarePackagesAsync(cancellationToken);
    }

    private async Task SeedSoftwarePackagesAsync(CancellationToken cancellationToken)
    {
        var readRepository = unitOfWork.GetReadRepository<SoftwarePackage>();
        var writeRepository = unitOfWork.GetWriteRepository<SoftwarePackage>();

        if (await readRepository.ExistsAsync(x => true, cancellationToken))
            return;

        logger.LogInformation("Seeding lightweight software packages for testing deployment pipelines...");

        var packages = new List<SoftwarePackage>
        {
            CreatePackageWithVersion("htop", "Interactive process viewer for Linux", "3.3.0", PackageType.Deb,
                OperatingSystemFamily.Ubuntu, "24.04", CpuArchitecture.Amd64),
            CreatePackageWithVersion("tree", "Displays directory paths in a tree-like format", "2.1.1", PackageType.Deb,
                OperatingSystemFamily.Ubuntu, "24.04", CpuArchitecture.Amd64),
            CreatePackageWithVersion("jq", "Command-line JSON processor", "1.7.1", PackageType.Deb,
                OperatingSystemFamily.Ubuntu, "24.04", CpuArchitecture.Amd64)
        };

        await writeRepository.AddRangeAsync(packages, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static SoftwarePackage CreatePackageWithVersion(
        string name,
        string description,
        string version,
        PackageType packageType,
        OperatingSystemFamily osFamily,
        string osVersion,
        CpuArchitecture architecture)
    {
        var package = new SoftwarePackage(name, description);

        var packageVersion = new SoftwarePackageVersion(
            package.Id,
            version,
            packageType,
            osFamily,
            osVersion,
            architecture);

        package.Versions.Add(packageVersion);
        return package;
    }
}