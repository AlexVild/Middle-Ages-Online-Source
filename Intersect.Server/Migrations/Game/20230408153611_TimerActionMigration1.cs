using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class TimerActionMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionType",
                table: "Timers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionVariableChangeValue",
                table: "Timers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ActionVariableId",
                table: "Timers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "ActionsEnabled",
                table: "Timers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "InstanceVariableActionType",
                table: "Timers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NValue",
                table: "Timers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "ActionVariableChangeValue",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "ActionVariableId",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "ActionsEnabled",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "InstanceVariableActionType",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "NValue",
                table: "Timers");
        }
    }
}
