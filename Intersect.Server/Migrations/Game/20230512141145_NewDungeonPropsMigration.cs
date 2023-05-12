using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class NewDungeonPropsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IgnoreCompletionEvents",
                table: "Dungeons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IgnoreStartEvents",
                table: "Dungeons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StoreLongestTime",
                table: "Dungeons",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IgnoreCompletionEvents",
                table: "Dungeons");

            migrationBuilder.DropColumn(
                name: "IgnoreStartEvents",
                table: "Dungeons");

            migrationBuilder.DropColumn(
                name: "StoreLongestTime",
                table: "Dungeons");
        }
    }
}
