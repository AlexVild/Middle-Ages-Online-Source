using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class NpcChampionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChampionCooldownSeconds",
                table: "Npcs",
                nullable: false,
                defaultValue: 600L);

            migrationBuilder.AddColumn<Guid>(
                name: "ChampionId",
                table: "Npcs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<float>(
                name: "ChampionSpawnChance",
                table: "Npcs",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChampionCooldownSeconds",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "ChampionId",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "ChampionSpawnChance",
                table: "Npcs");
        }
    }
}
