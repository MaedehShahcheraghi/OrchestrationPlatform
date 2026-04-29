using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace OrchestrationPlatform.Infrastructure.Persistence.Contexts;

public sealed class OrchestrationReadDbContext : DbContext
{
    public OrchestrationReadDbContext(DbContextOptions<OrchestrationReadDbContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("read");

        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type.Namespace is not null &&
                    type.Namespace.Contains(".Configurations.Read"));

        base.OnModelCreating(modelBuilder);
    }
}