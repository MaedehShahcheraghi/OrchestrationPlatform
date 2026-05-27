using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrchestrationPlatform.Domain.Entities;

namespace OrchestrationPlatform.Infrastructure.Persistence.Configurations;

public sealed class PackageArtifactConfiguration : IEntityTypeConfiguration<PackageArtifact>
{
    public void Configure(EntityTypeBuilder<PackageArtifact> builder)
    {
        builder.ToTable("PackageArtifacts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.SoftwarePackageVersionId)
            .IsRequired();

        builder.Property(x => x.BucketName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ObjectKey)
            .IsRequired()
            .HasMaxLength(1024);

        builder.Property(x => x.OriginalFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.ContentType)
            .HasMaxLength(150);

        builder.Property(x => x.Sha256Hash)
            .HasMaxLength(64);

        builder.Property(x => x.UploadedAtUtc)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(x => x.SoftwarePackageVersionId)
            .IsUnique()
            .HasDatabaseName("UX_PackageArtifacts_SoftwarePackageVersionId");

        builder.HasIndex(x => new { x.BucketName, x.ObjectKey })
            .IsUnique()
            .HasDatabaseName("UX_PackageArtifacts_BucketName_ObjectKey");

        builder.HasOne(x => x.SoftwarePackageVersion)
            .WithOne(x => x.Artifact)
            .HasForeignKey<PackageArtifact>(x => x.SoftwarePackageVersionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}