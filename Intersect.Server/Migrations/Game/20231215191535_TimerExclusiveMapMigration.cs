using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class TimerExclusiveMapMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExclusiveMaps",
                table: "Timers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SinglePlayerCancellation",
                table: "Timers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SinglePlayerCompletion",
                table: "Timers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SinglePlayerExpire",
                table: "Timers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExclusiveMaps",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "SinglePlayerCancellation",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "SinglePlayerCompletion",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "SinglePlayerExpire",
                table: "Timers");
        }
    }
}
