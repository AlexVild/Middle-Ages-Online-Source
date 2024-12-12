using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class AreaDenialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Combat_AoeTrapIntensity",
                table: "Spells",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Combat_AoeTrapRadiusOverride",
                table: "Spells",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Combat_AoeTrapSpawner",
                table: "Spells",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Combat_AoeTrapIntensity",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Combat_AoeTrapRadiusOverride",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Combat_AoeTrapSpawner",
                table: "Spells");
        }
    }
}
