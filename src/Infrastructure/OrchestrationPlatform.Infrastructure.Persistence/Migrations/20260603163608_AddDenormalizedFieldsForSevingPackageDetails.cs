using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrchestrationPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDenormalizedFieldsForSevingPackageDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackageNameSnapshot",
                schema: "orchestration",
                table: "InstallOperations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VersionSnapshot",
                schema: "orchestration",
                table: "InstallOperations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageNameSnapshot",
                schema: "orchestration",
                table: "InstallOperations");

            migrationBuilder.DropColumn(
                name: "VersionSnapshot",
                schema: "orchestration",
                table: "InstallOperations");
        }
    }
}
