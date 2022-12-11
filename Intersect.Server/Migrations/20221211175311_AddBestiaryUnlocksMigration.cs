using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class AddBestiaryUnlocksMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player_Bestiary_Unlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnlockType = table.Column<int>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    NpcId = table.Column<Guid>(nullable: false),
                    Unlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Bestiary_Unlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Bestiary_Unlocks_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_Bestiary_Unlocks_PlayerId",
                table: "Player_Bestiary_Unlocks",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player_Bestiary_Unlocks");
        }
    }
}
