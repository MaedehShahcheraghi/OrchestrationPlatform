using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrchestrationPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddotherOperationtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstalledSoftwares_InstallOperations_InstallOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_InstallOperations_InstallOperationId",
                schema: "orchestration",
                table: "OperationLogs");

            migrationBuilder.DropTable(
                name: "InstallOperations",
                schema: "orchestration");

            migrationBuilder.RenameColumn(
                name: "InstallOperationId",
                schema: "orchestration",
                table: "OperationLogs",
                newName: "OrchestrationOperationId");

            migrationBuilder.CreateTable(
                name: "OrchestrationOperations",
                schema: "orchestration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoftwarePackageVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OperatingSystemHostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProgressPercent = table.Column<int>(type: "int", nullable: false),
                    RequestedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitCode = table.Column<int>(type: "int", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalWorkflowId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PackageNameSnapshot = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VersionSnapshot = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PayloadJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrchestrationOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrchestrationOperations_OperatingSystemHosts_OperatingSystemHostId",
                        column: x => x.OperatingSystemHostId,
                        principalSchema: "orchestration",
                        principalTable: "OperatingSystemHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrchestrationOperations_SoftwarePackageVersions_SoftwarePackageVersionId",
                        column: x => x.SoftwarePackageVersionId,
                        principalSchema: "orchestration",
                        principalTable: "SoftwarePackageVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrchestrationOperations_HostId_RequestedAtUtc",
                schema: "orchestration",
                table: "OrchestrationOperations",
                columns: new[] { "OperatingSystemHostId", "RequestedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_OrchestrationOperations_IsDeleted",
                schema: "orchestration",
                table: "OrchestrationOperations",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrchestrationOperations_PackageVersionId_HostId",
                schema: "orchestration",
                table: "OrchestrationOperations",
                columns: new[] { "SoftwarePackageVersionId", "OperatingSystemHostId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrchestrationOperations_RequestedAtUtc",
                schema: "orchestration",
                table: "OrchestrationOperations",
                column: "RequestedAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_OrchestrationOperations_Status",
                schema: "orchestration",
                table: "OrchestrationOperations",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_InstalledSoftwares_OrchestrationOperations_InstallOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                column: "InstallOperationId",
                principalSchema: "orchestration",
                principalTable: "OrchestrationOperations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_OrchestrationOperations_OrchestrationOperationId",
                schema: "orchestration",
                table: "OperationLogs",
                column: "OrchestrationOperationId",
                principalSchema: "orchestration",
                principalTable: "OrchestrationOperations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstalledSoftwares_OrchestrationOperations_InstallOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares");

            migrationBuilder.DropForeignKey(
                name: "FK_OperationLogs_OrchestrationOperations_OrchestrationOperationId",
                schema: "orchestration",
                table: "OperationLogs");

            migrationBuilder.DropTable(
                name: "OrchestrationOperations",
                schema: "orchestration");

            migrationBuilder.RenameColumn(
                name: "OrchestrationOperationId",
                schema: "orchestration",
                table: "OperationLogs",
                newName: "InstallOperationId");

            migrationBuilder.CreateTable(
                name: "InstallOperations",
                schema: "orchestration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatingSystemHostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoftwarePackageVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnsibleInventoryPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AnsiblePlaybookPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitCode = table.Column<int>(type: "int", nullable: true),
                    ExternalWorkflowId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FinishedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    PackageNameSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgressPercent = table.Column<int>(type: "int", nullable: false),
                    RequestedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    VersionSnapshot = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallOperations_OperatingSystemHosts_OperatingSystemHostId",
                        column: x => x.OperatingSystemHostId,
                        principalSchema: "orchestration",
                        principalTable: "OperatingSystemHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstallOperations_SoftwarePackageVersions_SoftwarePackageVersionId",
                        column: x => x.SoftwarePackageVersionId,
                        principalSchema: "orchestration",
                        principalTable: "SoftwarePackageVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallOperations_HostId_RequestedAtUtc",
                schema: "orchestration",
                table: "InstallOperations",
                columns: new[] { "OperatingSystemHostId", "RequestedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_InstallOperations_IsDeleted",
                schema: "orchestration",
                table: "InstallOperations",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InstallOperations_PackageVersionId_HostId",
                schema: "orchestration",
                table: "InstallOperations",
                columns: new[] { "SoftwarePackageVersionId", "OperatingSystemHostId" });

            migrationBuilder.CreateIndex(
                name: "IX_InstallOperations_RequestedAtUtc",
                schema: "orchestration",
                table: "InstallOperations",
                column: "RequestedAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_InstallOperations_Status",
                schema: "orchestration",
                table: "InstallOperations",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_InstalledSoftwares_InstallOperations_InstallOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                column: "InstallOperationId",
                principalSchema: "orchestration",
                principalTable: "InstallOperations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OperationLogs_InstallOperations_InstallOperationId",
                schema: "orchestration",
                table: "OperationLogs",
                column: "InstallOperationId",
                principalSchema: "orchestration",
                principalTable: "InstallOperations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
