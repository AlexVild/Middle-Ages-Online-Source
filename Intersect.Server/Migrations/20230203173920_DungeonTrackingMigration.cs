using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class DungeonTrackingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player_Dungeons_Tracked",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    DungeonId = table.Column<Guid>(nullable: false),
                    SoloCompletions = table.Column<int>(nullable: false),
                    GroupCompletions = table.Column<int>(nullable: false),
                    Failures = table.Column<int>(nullable: false),
                    BestGroupTime = table.Column<long>(nullable: false),
                    BestSoloTime = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Dungeons_Tracked", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Dungeons_Tracked_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_Dungeons_Tracked_PlayerId",
                table: "Player_Dungeons_Tracked",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player_Dungeons_Tracked");
        }
    }
}
