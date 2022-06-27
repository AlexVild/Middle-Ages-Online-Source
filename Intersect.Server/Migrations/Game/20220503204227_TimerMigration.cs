using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class TimerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeCreated = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Hidden = table.Column<bool>(nullable: false),
                    Repetitions = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    OwnerType = table.Column<byte>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    CompletionBehavior = table.Column<byte>(nullable: false),
                    TimeLimit = table.Column<long>(nullable: false),
                    CancellationEventId = table.Column<Guid>(nullable: false),
                    ExpirationEventId = table.Column<Guid>(nullable: false),
                    CompletionEventId = table.Column<Guid>(nullable: false),
                    Folder = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timers");
        }
    }
}
