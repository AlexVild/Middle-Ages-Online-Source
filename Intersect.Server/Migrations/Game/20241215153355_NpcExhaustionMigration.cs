using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class NpcExhaustionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExhaustionCastTime",
                table: "Spells",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ExhaustionInterruptTime",
                table: "Spells",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExhaustionCastTime",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "ExhaustionInterruptTime",
                table: "Spells");
        }
    }
}
