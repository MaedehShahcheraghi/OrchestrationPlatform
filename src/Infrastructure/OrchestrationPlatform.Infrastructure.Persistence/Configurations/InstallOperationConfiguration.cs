using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Infrastructure.Persistence.Configurations;

public sealed class OrchestrationOperationConfiguration : IEntityTypeConfiguration<OrchestrationOperation>
{
    public void Configure(EntityTypeBuilder<OrchestrationOperation> builder)
    {
        builder.ToTable("OrchestrationOperations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.SoftwarePackageVersionId)
            .IsRequired(false);

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
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.ExternalWorkflowId)
            .HasMaxLength(200);

        builder.Property(x => x.PackageNameSnapshot)
            .HasMaxLength(200);

        builder.Property(x => x.VersionSnapshot)
            .HasMaxLength(100);

        builder.Property(x => x.PayloadJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.ExitCode);

        builder.HasOne(x => x.SoftwarePackageVersion)
            .WithMany(x => x.OrchestrationOperations)
            .HasForeignKey(x => x.SoftwarePackageVersionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.OperatingSystemHost)
            .WithMany(x => x.OrchestrationOperations)
            .HasForeignKey(x => x.OperatingSystemHostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Logs)
            .WithOne(x => x.OrchestrationOperation)
            .HasForeignKey(x => x.OrchestrationOperationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Status)
            .HasDatabaseName("IX_OrchestrationOperations_Status");

        builder.HasIndex(x => x.RequestedAtUtc)
            .HasDatabaseName("IX_OrchestrationOperations_RequestedAtUtc");

        builder.HasIndex(x => new { x.OperatingSystemHostId, x.RequestedAtUtc })
            .HasDatabaseName("IX_OrchestrationOperations_HostId_RequestedAtUtc");

        builder.HasIndex(x => new { x.SoftwarePackageVersionId, x.OperatingSystemHostId })
            .HasDatabaseName("IX_OrchestrationOperations_PackageVersionId_HostId");
    }
}