using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerVM.Services.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ManagerVM");

            migrationBuilder.CreateTable(
                name: "OpenStackEntities",
                schema: "ManagerVM",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndPointUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenStackEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantEntities",
                schema: "ManagerVM",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "ManagerVM",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                schema: "ManagerVM",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstanceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenStackId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entities_OpenStackEntities_OpenStackId",
                        column: x => x.OpenStackId,
                        principalSchema: "ManagerVM",
                        principalTable: "OpenStackEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entities_TenantEntities_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "ManagerVM",
                        principalTable: "TenantEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenStackInTenantEntities",
                schema: "ManagerVM",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenStackId = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenStackInTenantEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenStackInTenantEntities_OpenStackEntities_OpenStackId",
                        column: x => x.OpenStackId,
                        principalSchema: "ManagerVM",
                        principalTable: "OpenStackEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpenStackInTenantEntities_TenantEntities_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "ManagerVM",
                        principalTable: "TenantEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entities_OpenStackId",
                schema: "ManagerVM",
                table: "Entities",
                column: "OpenStackId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_TenantId",
                schema: "ManagerVM",
                table: "Entities",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenStackInTenantEntities_OpenStackId",
                schema: "ManagerVM",
                table: "OpenStackInTenantEntities",
                column: "OpenStackId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenStackInTenantEntities_TenantId",
                schema: "ManagerVM",
                table: "OpenStackInTenantEntities",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entities",
                schema: "ManagerVM");

            migrationBuilder.DropTable(
                name: "OpenStackInTenantEntities",
                schema: "ManagerVM");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "ManagerVM");

            migrationBuilder.DropTable(
                name: "OpenStackEntities",
                schema: "ManagerVM");

            migrationBuilder.DropTable(
                name: "TenantEntities",
                schema: "ManagerVM");
        }
    }
}
