using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Infrastructure.Persistence.Configurations;

public sealed class SoftwarePackageVersionConfiguration : IEntityTypeConfiguration<SoftwarePackageVersion>
{
    public void Configure(EntityTypeBuilder<SoftwarePackageVersion> builder)
    {
        builder.ToTable("SoftwarePackageVersions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.SoftwarePackageId)
            .IsRequired();

        builder.Property(x => x.Version)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.PackageType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.OperatingSystemFamily)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.OperatingSystemVersion)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Architecture)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(x => new
            {
                x.SoftwarePackageId,
                x.Version,
                x.PackageType,
                x.OperatingSystemFamily,
                x.OperatingSystemVersion,
                x.Architecture
            })
            .IsUnique()
            .HasDatabaseName("UX_SoftwarePackageVersions_UniqueVersion")
            .HasFilter("[IsDeleted] = 0");
        ;

        builder.HasOne(x => x.SoftwarePackage)
            .WithMany(x => x.Versions)
            .HasForeignKey(x => x.SoftwarePackageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Artifact)
            .WithOne(x => x.SoftwarePackageVersion)
            .HasForeignKey<PackageArtifact>(x => x.SoftwarePackageVersionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.InstallOperations)
            .WithOne(x => x.SoftwarePackageVersion)
            .HasForeignKey(x => x.SoftwarePackageVersionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.InstalledSoftwares)
            .WithOne(x => x.SoftwarePackageVersion)
            .HasForeignKey(x => x.SoftwarePackageVersionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}