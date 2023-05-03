using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class PermabuffStatsAndVitalsMigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PermabuffedStats",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermabuffedVitals",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermabuffedStats",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PermabuffedVitals",
                table: "Players");
        }
    }
}
