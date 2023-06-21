using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class NpcScalingVitalsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxScaledTo",
                table: "Npcs",
                nullable: false,
                defaultValue: 50);

            migrationBuilder.AddColumn<int>(
                name: "ScaledTo",
                table: "Npcs",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<float>(
                name: "VitalScaleModifier",
                table: "Npcs",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxScaledTo",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "ScaledTo",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "VitalScaleModifier",
                table: "Npcs");
        }
    }
}
