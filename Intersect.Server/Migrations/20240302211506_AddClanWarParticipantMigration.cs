using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class AddClanWarParticipantMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clan_War_Participants",
                columns: table => new
                {
                    GuildId = table.Column<Guid>(nullable: false),
                    ClanWarId = table.Column<Guid>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clan_War_Participants", x => new { x.ClanWarId, x.GuildId });
                    table.ForeignKey(
                        name: "FK_Clan_War_Participants_Clan_Wars_ClanWarId",
                        column: x => x.ClanWarId,
                        principalTable: "Clan_Wars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clan_War_Participants");
        }
    }
}
