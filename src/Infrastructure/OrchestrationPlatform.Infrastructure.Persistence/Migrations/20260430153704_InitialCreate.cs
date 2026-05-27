using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrchestrationPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "orchestration");

            migrationBuilder.CreateTable(
                name: "OperatingSystemHosts",
                schema: "orchestration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    SshPort = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OperatingSystemFamily = table.Column<int>(type: "int", nullable: false),
                    OperatingSystemVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Architecture = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SshKeyPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastConnectionError = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    LastSeenAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingSystemHosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoftwarePackages",
                schema: "orchestration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwarePackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoftwarePackageVersions",
                schema: "orchestration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoftwarePackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PackageType = table.Column<int>(type: "int", nullable: false),
                    OperatingSystemFamily = table.Column<int>(type: "int", nullable: false),
                    OperatingSystemVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Architecture = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwarePackageVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftwarePackageVersions_SoftwarePackages_SoftwarePackageId",
                        column: x => x.SoftwarePackageId,
                        principalSchema: "orchestration",
                        principalTable: "SoftwarePackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstallOperations",
                schema: "orchestration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoftwarePackageVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatingSystemHostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProgressPercent = table.Column<int>(type: "int", nullable: false),
                    RequestedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitCode = table.Column<int>(type: "int", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    AnsiblePlaybookPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AnsibleInventoryPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExternalWorkflowId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "PackageArtifacts",
                schema: "orchestration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoftwarePackageVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BucketName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ObjectKey = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Sha256Hash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UploadedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageArtifacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageArtifacts_SoftwarePackageVersions_SoftwarePackageVersionId",
                        column: x => x.SoftwarePackageVersionId,
                        principalSchema: "orchestration",
                        principalTable: "SoftwarePackageVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstalledSoftwares",
                schema: "orchestration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoftwarePackageVersionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatingSystemHostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstalledName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    InstalledVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InstalledAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemovedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstalledSoftwares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstalledSoftwares_InstallOperations_InstallOperationId",
                        column: x => x.InstallOperationId,
                        principalSchema: "orchestration",
                        principalTable: "InstallOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstalledSoftwares_OperatingSystemHosts_OperatingSystemHostId",
                        column: x => x.OperatingSystemHostId,
                        principalSchema: "orchestration",
                        principalTable: "OperatingSystemHosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstalledSoftwares_SoftwarePackageVersions_SoftwarePackageVersionId",
                        column: x => x.SoftwarePackageVersionId,
                        principalSchema: "orchestration",
                        principalTable: "SoftwarePackageVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationLogs",
                schema: "orchestration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstallOperationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoggedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationLogs_InstallOperations_InstallOperationId",
                        column: x => x.InstallOperationId,
                        principalSchema: "orchestration",
                        principalTable: "InstallOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstalledSoftwares_IsDeleted",
                schema: "orchestration",
                table: "InstalledSoftwares",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InstalledSoftwares_OperatingSystemHostId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                column: "OperatingSystemHostId");

            migrationBuilder.CreateIndex(
                name: "IX_InstalledSoftwares_SoftwarePackageVersionId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                column: "SoftwarePackageVersionId");

            migrationBuilder.CreateIndex(
                name: "UX_InstalledSoftwares_Host_InstalledName_Active",
                schema: "orchestration",
                table: "InstalledSoftwares",
                columns: new[] { "OperatingSystemHostId", "InstalledName" },
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "UX_InstalledSoftwares_InstallOperationId",
                schema: "orchestration",
                table: "InstalledSoftwares",
                column: "InstallOperationId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_OperatingSystemHosts_IsDeleted",
                schema: "orchestration",
                table: "OperatingSystemHosts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OperatingSystemHosts_Name",
                schema: "orchestration",
                table: "OperatingSystemHosts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "UX_OperatingSystemHosts_IpAddress_SshPort",
                schema: "orchestration",
                table: "OperatingSystemHosts",
                columns: new[] { "IpAddress", "SshPort" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_InstallOperationId_LoggedAtUtc",
                schema: "orchestration",
                table: "OperationLogs",
                columns: new[] { "InstallOperationId", "LoggedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_IsDeleted",
                schema: "orchestration",
                table: "OperationLogs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_Level",
                schema: "orchestration",
                table: "OperationLogs",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_PackageArtifacts_IsDeleted",
                schema: "orchestration",
                table: "PackageArtifacts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_PackageArtifacts_BucketName_ObjectKey",
                schema: "orchestration",
                table: "PackageArtifacts",
                columns: new[] { "BucketName", "ObjectKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_PackageArtifacts_SoftwarePackageVersionId",
                schema: "orchestration",
                table: "PackageArtifacts",
                column: "SoftwarePackageVersionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoftwarePackages_IsDeleted",
                schema: "orchestration",
                table: "SoftwarePackages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_SoftwarePackages_Name",
                schema: "orchestration",
                table: "SoftwarePackages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoftwarePackageVersions_IsDeleted",
                schema: "orchestration",
                table: "SoftwarePackageVersions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_SoftwarePackageVersions_UniqueVersion",
                schema: "orchestration",
                table: "SoftwarePackageVersions",
                columns: new[] { "SoftwarePackageId", "Version", "PackageType", "OperatingSystemFamily", "OperatingSystemVersion", "Architecture" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstalledSoftwares",
                schema: "orchestration");

            migrationBuilder.DropTable(
                name: "OperationLogs",
                schema: "orchestration");

            migrationBuilder.DropTable(
                name: "PackageArtifacts",
                schema: "orchestration");

            migrationBuilder.DropTable(
                name: "InstallOperations",
                schema: "orchestration");

            migrationBuilder.DropTable(
                name: "OperatingSystemHosts",
                schema: "orchestration");

            migrationBuilder.DropTable(
                name: "SoftwarePackageVersions",
                schema: "orchestration");

            migrationBuilder.DropTable(
                name: "SoftwarePackages",
                schema: "orchestration");
        }
    }
}
