using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class NpcVitalStealMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeleeLifesteal",
                table: "Npcs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeleeManasteal",
                table: "Npcs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeleeLifesteal",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "MeleeManasteal",
                table: "Npcs");
        }
    }
}
