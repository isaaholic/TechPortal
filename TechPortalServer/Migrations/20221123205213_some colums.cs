using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechPortalServer.Migrations
{
    public partial class somecolums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Range",
                table: "WorkExperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Patents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Inventions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Range",
                table: "WorkExperiences");

            migrationBuilder.DropColumn(
                name: "About",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "About",
                table: "Patents");

            migrationBuilder.DropColumn(
                name: "About",
                table: "Inventions");
        }
    }
}
