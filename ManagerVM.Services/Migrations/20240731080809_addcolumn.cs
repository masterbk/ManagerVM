using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerVM.Services.Migrations
{
    /// <inheritdoc />
    public partial class addcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HostName",
                schema: "ManagerVM",
                table: "VMEntities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostName",
                schema: "ManagerVM",
                table: "VMEntities");
        }
    }
}
