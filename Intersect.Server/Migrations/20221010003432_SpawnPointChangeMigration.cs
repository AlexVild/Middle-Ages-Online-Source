using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class SpawnPointChangeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArenaRespawnDir",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ArenaRespawn",
                table: "Players",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ArenaRespawnX",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ArenaRespawnY",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RespawnOverrideDir",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "RespawnOverride",
                table: "Players",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "RespawnOverrideX",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RespawnOverrideY",
                table: "Players",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArenaRespawnDir",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ArenaRespawn",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ArenaRespawnX",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ArenaRespawnY",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RespawnOverrideDir",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RespawnOverride",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RespawnOverrideX",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RespawnOverrideY",
                table: "Players");
        }
    }
}
