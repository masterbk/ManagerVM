using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerVM.Services.Migrations
{
    /// <inheritdoc />
    public partial class Alter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InstallAllServiceSuccess",
                schema: "ManagerVM",
                table: "VMEntities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "RoomId",
                schema: "ManagerVM",
                table: "VMEntities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallAllServiceSuccess",
                schema: "ManagerVM",
                table: "VMEntities");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "ManagerVM",
                table: "VMEntities");
        }
    }
}
