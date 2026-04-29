using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace OrchestrationPlatform.Infrastructure.Persistence.Contexts;

public sealed class OrchestrationWriteDbContext : DbContext
{
    public OrchestrationWriteDbContext(DbContextOptions<OrchestrationWriteDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("write");

        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type.Namespace is not null &&
                    type.Namespace.Contains(".Configurations.Write"));

        base.OnModelCreating(modelBuilder);
    }
}