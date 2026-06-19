using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Infrastructure.Persistence.Configurations;

public sealed class OperatingSystemHostConfiguration : IEntityTypeConfiguration<OperatingSystemHost>
{
    public void Configure(EntityTypeBuilder<OperatingSystemHost> builder)
    {
        builder.ToTable("OperatingSystemHosts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.IpAddress)
            .IsRequired()
            .HasMaxLength(45);

        builder.Property(x => x.SshPort)
            .IsRequired();

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.OperatingSystemFamily)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.OperatingSystemVersion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Architecture)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.SshKeyPath)
            .HasMaxLength(500);

        builder.Property(x => x.LastConnectionError)
            .HasMaxLength(2000);

        builder.Property(x => x.LastSeenAtUtc)
            .HasColumnType("datetime2");

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(x => x.Name)
            .HasDatabaseName("IX_OperatingSystemHosts_Name");

        builder.HasIndex(x => new { x.IpAddress, x.SshPort })
            .IsUnique()
            .HasDatabaseName("UX_OperatingSystemHosts_IpAddress_SshPort")
            .HasFilter("[IsDeleted] = 0");
        ;

        builder.HasMany(x => x.OrchestrationOperations)
            .WithOne(x => x.OperatingSystemHost)
            .HasForeignKey(x => x.OperatingSystemHostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.InstalledSoftwares)
            .WithOne(x => x.OperatingSystemHost)
            .HasForeignKey(x => x.OperatingSystemHostId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}