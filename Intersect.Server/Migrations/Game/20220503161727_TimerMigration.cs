using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class TimerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstanceTimers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeCreated = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FailureEvent = table.Column<Guid>(nullable: false),
                    StopEvent = table.Column<Guid>(nullable: false),
                    InstanceVariableStore = table.Column<Guid>(nullable: false),
                    Folder = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceTimers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTimers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeCreated = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FailureEvent = table.Column<Guid>(nullable: false),
                    StopEvent = table.Column<Guid>(nullable: false),
                    PlayerVariableStore = table.Column<Guid>(nullable: false),
                    Folder = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTimers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstanceTimers");

            migrationBuilder.DropTable(
                name: "PlayerTimers");
        }
    }
}
