using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrchestrationPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AFDDfilterforsoftdelete_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_SoftwarePackageVersions_UniqueVersion",
                schema: "orchestration",
                table: "SoftwarePackageVersions");

            migrationBuilder.CreateIndex(
                name: "UX_SoftwarePackageVersions_UniqueVersion",
                schema: "orchestration",
                table: "SoftwarePackageVersions",
                columns: new[] { "SoftwarePackageId", "Version", "PackageType", "OperatingSystemFamily", "OperatingSystemVersion", "Architecture" },
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_SoftwarePackageVersions_UniqueVersion",
                schema: "orchestration",
                table: "SoftwarePackageVersions");

            migrationBuilder.CreateIndex(
                name: "UX_SoftwarePackageVersions_UniqueVersion",
                schema: "orchestration",
                table: "SoftwarePackageVersions",
                columns: new[] { "SoftwarePackageId", "Version", "PackageType", "OperatingSystemFamily", "OperatingSystemVersion", "Architecture" },
                unique: true);
        }
    }
}
