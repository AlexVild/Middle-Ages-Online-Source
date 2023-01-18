using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class NewChallengePropertiesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChallengeParamId",
                table: "Challenges",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ChallengeParamType",
                table: "Challenges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CompletionEventId",
                table: "Challenges",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "EventDescription",
                table: "Challenges",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChallengeParamId",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "ChallengeParamType",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "CompletionEventId",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "EventDescription",
                table: "Challenges");
        }
    }
}
