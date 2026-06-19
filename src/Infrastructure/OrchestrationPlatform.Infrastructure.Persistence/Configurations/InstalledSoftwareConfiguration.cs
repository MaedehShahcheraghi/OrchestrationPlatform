using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Infrastructure.Persistence.Configurations;

public sealed class InstalledSoftwareConfiguration : IEntityTypeConfiguration<InstalledSoftware>
{
    public void Configure(EntityTypeBuilder<InstalledSoftware> builder)
    {
        builder.ToTable("InstalledSoftwares");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SoftwarePackageVersionId)
            .IsRequired();

        builder.Property(x => x.OperatingSystemHostId)
            .IsRequired();

        builder.Property(x => x.OrchestrationOperationId)
            .IsRequired();

        builder.Property(x => x.InstalledName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.InstalledVersion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.InstalledAtUtc)
            .IsRequired();

        builder.Property(x => x.RemovedAtUtc);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasOne(x => x.SoftwarePackageVersion)
            .WithMany(x => x.InstalledSoftwares)
            .HasForeignKey(x => x.SoftwarePackageVersionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.OperatingSystemHost)
            .WithMany(x => x.InstalledSoftwares)
            .HasForeignKey(x => x.OperatingSystemHostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.OrchestrationOperation)
            .WithMany()
            .HasForeignKey(x => x.OrchestrationOperationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.SoftwarePackageVersionId)
            .HasDatabaseName("IX_InstalledSoftwares_SoftwarePackageVersionId");

        builder.HasIndex(x => x.OperatingSystemHostId)
            .HasDatabaseName("IX_InstalledSoftwares_OperatingSystemHostId");

        builder.HasIndex(x => x.OrchestrationOperationId)
            .IsUnique()
            .HasDatabaseName("UX_InstalledSoftwares_OrchestrationOperationId");

        builder.HasIndex(x => new { x.OperatingSystemHostId, x.InstalledName })
            .IsUnique()
            .HasFilter("[IsActive] = 1")
            .HasDatabaseName("UX_InstalledSoftwares_Host_InstalledName_Active");
    }
}