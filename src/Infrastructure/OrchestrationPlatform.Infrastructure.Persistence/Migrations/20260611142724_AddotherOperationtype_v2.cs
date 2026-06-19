using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrchestrationPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddotherOperationtype_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstalledSoftwares_OrchestrationOperations_InstallOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares");

            migrationBuilder.RenameColumn(
                name: "InstallOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                newName: "OrchestrationOperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstalledSoftwares_OrchestrationOperations_OrchestrationOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                column: "OrchestrationOperationId",
                principalSchema: "orchestration",
                principalTable: "OrchestrationOperations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstalledSoftwares_OrchestrationOperations_OrchestrationOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares");

            migrationBuilder.RenameColumn(
                name: "OrchestrationOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                newName: "InstallOperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstalledSoftwares_OrchestrationOperations_InstallOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                column: "InstallOperationId",
                principalSchema: "orchestration",
                principalTable: "OrchestrationOperations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
