using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class OpenMeleeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InDuel",
                table: "Players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "MeleeEndMapId",
                table: "Players",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "MeleeEndX",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MeleeEndY",
                table: "Players",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InDuel",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MeleeEndMapId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MeleeEndX",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MeleeEndY",
                table: "Players");
        }
    }
}
