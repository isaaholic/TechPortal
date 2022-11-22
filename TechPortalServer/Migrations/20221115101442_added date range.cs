using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechPortalServer.Migrations
{
    public partial class addeddaterange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Range",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Range",
                table: "Educations");
        }
    }
}
