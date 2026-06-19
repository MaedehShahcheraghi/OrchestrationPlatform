using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrchestrationPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddotherOperationtype_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UX_InstalledSoftwares_InstallOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                newName: "UX_InstalledSoftwares_OrchestrationOperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "UX_InstalledSoftwares_OrchestrationOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                newName: "UX_InstalledSoftwares_InstallOperationId");
        }
    }
}
