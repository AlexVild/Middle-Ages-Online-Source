using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class DungeonScalingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApplyStatCeiling",
                table: "Dungeons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StatCeilingTier",
                table: "Dungeons",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyStatCeiling",
                table: "Dungeons");

            migrationBuilder.DropColumn(
                name: "StatCeilingTier",
                table: "Dungeons");
        }
    }
}
