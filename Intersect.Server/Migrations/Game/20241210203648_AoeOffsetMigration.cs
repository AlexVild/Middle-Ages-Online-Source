using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class AoeOffsetMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Combat_AoeRelativeOffset",
                table: "Spells",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Combat_AoeXOffset",
                table: "Spells",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Combat_AoeYOffset",
                table: "Spells",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Combat_AoeRelativeOffset",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Combat_AoeXOffset",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Combat_AoeYOffset",
                table: "Spells");
        }
    }
}
