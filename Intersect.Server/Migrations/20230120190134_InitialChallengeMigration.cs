using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class InitialChallengeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player_Challenges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    Challenge = table.Column<Guid>(nullable: false),
                    Complete = table.Column<bool>(nullable: false),
                    Progress = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Challenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Challenges_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player_Weapon_Masteries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    WeaponType = table.Column<Guid>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    ExpRemaining = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player_Weapon_Masteries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Weapon_Masteries_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_Challenges_PlayerId",
                table: "Player_Challenges",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_Weapon_Masteries_PlayerId",
                table: "Player_Weapon_Masteries",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player_Challenges");

            migrationBuilder.DropTable(
                name: "Player_Weapon_Masteries");
        }
    }
}
