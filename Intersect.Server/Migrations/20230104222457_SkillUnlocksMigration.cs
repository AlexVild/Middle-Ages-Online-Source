using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class SkillUnlocksMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillPointTotal",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Player_Unlocked_Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SpellId = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    Equipped = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Unlocked_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Unlocked_Skills_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_Unlocked_Skills_PlayerId",
                table: "Player_Unlocked_Skills",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player_Unlocked_Skills");

            migrationBuilder.DropColumn(
                name: "SkillPointTotal",
                table: "Players");
        }
    }
}
