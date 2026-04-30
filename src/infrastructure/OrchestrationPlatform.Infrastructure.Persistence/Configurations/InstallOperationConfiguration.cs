using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Infrastructure.Persistence.Configurations;

public sealed class InstallOperationConfiguration : IEntityTypeConfiguration<InstallOperation>
{
    public void Configure(EntityTypeBuilder<InstallOperation> builder)
    {
        builder.ToTable("InstallOperations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.SoftwarePackageVersionId)
            .IsRequired();

        builder.Property(x => x.OperatingSystemHostId)
            .IsRequired();

        builder.Property(x => x.OperationType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.ProgressPercent)
            .IsRequired();

        builder.Property(x => x.RequestedAtUtc)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.StartedAtUtc)
            .HasColumnType("datetime2");

        builder.Property(x => x.FinishedAtUtc)
            .HasColumnType("datetime2");

        builder.Property(x => x.ErrorMessage)
            .HasMaxLength(4000);

        builder.Property(x => x.AnsiblePlaybookPath)
            .HasMaxLength(500);

        builder.Property(x => x.AnsibleInventoryPath)
            .HasMaxLength(500);

        builder.Property(x => x.ExternalWorkflowId)
            .HasMaxLength(200);

        builder.Property(x => x.ExitCode);

        builder.HasOne(x => x.SoftwarePackageVersion)
            .WithMany(x => x.InstallOperations)
            .HasForeignKey(x => x.SoftwarePackageVersionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.OperatingSystemHost)
            .WithMany(x => x.InstallOperations)
            .HasForeignKey(x => x.OperatingSystemHostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Logs)
            .WithOne(x => x.InstallOperation)
            .HasForeignKey(x => x.InstallOperationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Status)
            .HasDatabaseName("IX_InstallOperations_Status");

        builder.HasIndex(x => x.RequestedAtUtc)
            .HasDatabaseName("IX_InstallOperations_RequestedAtUtc");

        builder.HasIndex(x => new { x.OperatingSystemHostId, x.RequestedAtUtc })
            .HasDatabaseName("IX_InstallOperations_HostId_RequestedAtUtc");

        builder.HasIndex(x => new { x.SoftwarePackageVersionId, x.OperatingSystemHostId })
            .HasDatabaseName("IX_InstallOperations_PackageVersionId_HostId");
    }
}