using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerVM.Services.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_OpenStackEntities_OpenStackId",
                schema: "ManagerVM",
                table: "Entities");

            migrationBuilder.DropForeignKey(
                name: "FK_Entities_TenantEntities_TenantId",
                schema: "ManagerVM",
                table: "Entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entities",
                schema: "ManagerVM",
                table: "Entities");

            migrationBuilder.RenameTable(
                name: "Entities",
                schema: "ManagerVM",
                newName: "VMEntities",
                newSchema: "ManagerVM");

            migrationBuilder.RenameIndex(
                name: "IX_Entities_TenantId",
                schema: "ManagerVM",
                table: "VMEntities",
                newName: "IX_VMEntities_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Entities_OpenStackId",
                schema: "ManagerVM",
                table: "VMEntities",
                newName: "IX_VMEntities_OpenStackId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "ManagerVM",
                table: "TenantEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VMEntities",
                schema: "ManagerVM",
                table: "VMEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VMEntities_OpenStackEntities_OpenStackId",
                schema: "ManagerVM",
                table: "VMEntities",
                column: "OpenStackId",
                principalSchema: "ManagerVM",
                principalTable: "OpenStackEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VMEntities_TenantEntities_TenantId",
                schema: "ManagerVM",
                table: "VMEntities",
                column: "TenantId",
                principalSchema: "ManagerVM",
                principalTable: "TenantEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VMEntities_OpenStackEntities_OpenStackId",
                schema: "ManagerVM",
                table: "VMEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_VMEntities_TenantEntities_TenantId",
                schema: "ManagerVM",
                table: "VMEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VMEntities",
                schema: "ManagerVM",
                table: "VMEntities");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "ManagerVM",
                table: "TenantEntities");

            migrationBuilder.RenameTable(
                name: "VMEntities",
                schema: "ManagerVM",
                newName: "Entities",
                newSchema: "ManagerVM");

            migrationBuilder.RenameIndex(
                name: "IX_VMEntities_TenantId",
                schema: "ManagerVM",
                table: "Entities",
                newName: "IX_Entities_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_VMEntities_OpenStackId",
                schema: "ManagerVM",
                table: "Entities",
                newName: "IX_Entities_OpenStackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entities",
                schema: "ManagerVM",
                table: "Entities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_OpenStackEntities_OpenStackId",
                schema: "ManagerVM",
                table: "Entities",
                column: "OpenStackId",
                principalSchema: "ManagerVM",
                principalTable: "OpenStackEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_TenantEntities_TenantId",
                schema: "ManagerVM",
                table: "Entities",
                column: "TenantId",
                principalSchema: "ManagerVM",
                principalTable: "TenantEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
