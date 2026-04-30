using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Infrastructure.Persistence.Configurations;

public sealed class OperationLogConfiguration : IEntityTypeConfiguration<OperationLog>
{
    public void Configure(EntityTypeBuilder<OperationLog> builder)
    {
        builder.ToTable("OperationLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.InstallOperationId)
            .IsRequired();

        builder.Property(x => x.Level)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Details);

        builder.Property(x => x.LoggedAtUtc)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.HasOne(x => x.InstallOperation)
            .WithMany(x => x.Logs)
            .HasForeignKey(x => x.InstallOperationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.InstallOperationId, x.LoggedAtUtc })
            .HasDatabaseName("IX_OperationLogs_InstallOperationId_LoggedAtUtc");

        builder.HasIndex(x => x.Level)
            .HasDatabaseName("IX_OperationLogs_Level");
    }
}