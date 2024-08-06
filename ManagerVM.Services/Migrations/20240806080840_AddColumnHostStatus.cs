using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerVM.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnHostStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HostStatus",
                schema: "ManagerVM",
                table: "VMEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostStatus",
                schema: "ManagerVM",
                table: "VMEntities");
        }
    }
}
