using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class GuildInstanceIdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LastOverworldMapId",
                table: "Players",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "LastOverworldX",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastOverworldY",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "GuildInstanceId",
                table: "Guilds",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastOverworldMapId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastOverworldX",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastOverworldY",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GuildInstanceId",
                table: "Guilds");
        }
    }
}
