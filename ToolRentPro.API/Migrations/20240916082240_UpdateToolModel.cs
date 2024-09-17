using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolRentPro.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToolModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalTool",
                table: "Tools");

            migrationBuilder.CreateTable(
                name: "RentalTool",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ToolModelId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalTool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalTool_Tools_ToolModelId",
                        column: x => x.ToolModelId,
                        principalTable: "Tools",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RentalTool_ToolModelId",
                table: "RentalTool",
                column: "ToolModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalTool");

            migrationBuilder.AddColumn<string>(
                name: "RentalTool",
                table: "Tools",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
