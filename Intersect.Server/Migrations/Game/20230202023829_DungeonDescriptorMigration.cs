using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class DungeonDescriptorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dungeons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeCreated = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    TimerId = table.Column<Guid>(nullable: false),
                    GnomeLocations = table.Column<int>(nullable: false),
                    CompletionCounterId = table.Column<Guid>(nullable: false),
                    Treasure = table.Column<string>(nullable: true),
                    ExpRewards = table.Column<string>(nullable: true),
                    TimeRequirements = table.Column<string>(nullable: true),
                    Folder = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dungeons", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dungeons");
        }
    }
}
