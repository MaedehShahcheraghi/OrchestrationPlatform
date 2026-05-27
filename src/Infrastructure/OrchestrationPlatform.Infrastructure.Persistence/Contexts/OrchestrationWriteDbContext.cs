using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OrchestrationPlatform.Domain.Entities;
using OrchestrationPlatform.Infrastructure.Persistence.Extensions;

namespace OrchestrationPlatform.Infrastructure.Persistence.Contexts;

public sealed class OrchestrationWriteDbContext : DbContext
{
    public OrchestrationWriteDbContext(DbContextOptions<OrchestrationWriteDbContext> options)
        : base(options)
    {
    }

    public DbSet<SoftwarePackage> SoftwarePackages => Set<SoftwarePackage>();

    public DbSet<SoftwarePackageVersion> SoftwarePackageVersions => Set<SoftwarePackageVersion>();

    public DbSet<PackageArtifact> PackageArtifacts => Set<PackageArtifact>();

    public DbSet<OperatingSystemHost> OperatingSystemHosts => Set<OperatingSystemHost>();

    public DbSet<InstallOperation> InstallOperations => Set<InstallOperation>();

    public DbSet<OperationLog> OperationLogs => Set<OperationLog>();

    public DbSet<InstalledSoftware> InstalledSoftwares => Set<InstalledSoftware>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("orchestration");

        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type.Namespace is not null &&
                    type.Namespace.Contains(".Configurations"));

        modelBuilder.ApplySoftDeleteQueryFilters();

        base.OnModelCreating(modelBuilder);
    }
}