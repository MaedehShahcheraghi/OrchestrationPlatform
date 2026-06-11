using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrchestrationPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AFDDfilterforsoftdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_SoftwarePackages_Name",
                schema: "orchestration",
                table: "SoftwarePackages");

            migrationBuilder.DropIndex(
                name: "UX_PackageArtifacts_BucketName_ObjectKey",
                schema: "orchestration",
                table: "PackageArtifacts");

            migrationBuilder.DropIndex(
                name: "UX_OperatingSystemHosts_IpAddress_SshPort",
                schema: "orchestration",
                table: "OperatingSystemHosts");

            migrationBuilder.CreateIndex(
                name: "UX_SoftwarePackages_Name",
                schema: "orchestration",
                table: "SoftwarePackages",
                column: "Name",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "UX_PackageArtifacts_BucketName_ObjectKey",
                schema: "orchestration",
                table: "PackageArtifacts",
                columns: new[] { "BucketName", "ObjectKey" },
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "UX_OperatingSystemHosts_IpAddress_SshPort",
                schema: "orchestration",
                table: "OperatingSystemHosts",
                columns: new[] { "IpAddress", "SshPort" },
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_SoftwarePackages_Name",
                schema: "orchestration",
                table: "SoftwarePackages");

            migrationBuilder.DropIndex(
                name: "UX_PackageArtifacts_BucketName_ObjectKey",
                schema: "orchestration",
                table: "PackageArtifacts");

            migrationBuilder.DropIndex(
                name: "UX_OperatingSystemHosts_IpAddress_SshPort",
                schema: "orchestration",
                table: "OperatingSystemHosts");

            migrationBuilder.CreateIndex(
                name: "UX_SoftwarePackages_Name",
                schema: "orchestration",
                table: "SoftwarePackages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_PackageArtifacts_BucketName_ObjectKey",
                schema: "orchestration",
                table: "PackageArtifacts",
                columns: new[] { "BucketName", "ObjectKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_OperatingSystemHosts_IpAddress_SshPort",
                schema: "orchestration",
                table: "OperatingSystemHosts",
                columns: new[] { "IpAddress", "SshPort" },
                unique: true);
        }
    }
}
