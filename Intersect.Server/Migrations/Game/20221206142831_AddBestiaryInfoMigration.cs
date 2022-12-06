using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class AddBestiaryInfoMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BestiaryUnlocks",
                table: "Npcs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Npcs",
                nullable: false,
                defaultValue: string.Empty);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestiaryUnlocks",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Npcs");
        }
    }
}
