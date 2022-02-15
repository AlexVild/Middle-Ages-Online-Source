using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class AddSharedInstanceRespawnPointMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SharedInstanceRespawnDir",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SharedInstanceRespawnId",
                table: "Players",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SharedInstanceRespawnX",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SharedInstanceRespawnY",
                table: "Players",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharedInstanceRespawnDir",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "SharedInstanceRespawnId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "SharedInstanceRespawnX",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "SharedInstanceRespawnY",
                table: "Players");
        }
    }
}
