using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolRentPro.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tools",
                newName: "NameTool");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameTool",
                table: "Tools",
                newName: "Name");
        }
    }
}
