using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class ClanWarInstanceMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clan_Wars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeStarted = table.Column<long>(nullable: false),
                    TimeEnded = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clan_Wars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Territories",
                columns: table => new
                {
                    TerritoryId = table.Column<Guid>(nullable: false),
                    GuildId = table.Column<Guid>(nullable: false),
                    ClanWarId = table.Column<Guid>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territories", x => new { x.TerritoryId, x.ClanWarId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clan_Wars");

            migrationBuilder.DropTable(
                name: "Territories");
        }
    }
}
