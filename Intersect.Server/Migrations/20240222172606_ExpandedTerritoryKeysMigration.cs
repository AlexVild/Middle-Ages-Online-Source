using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class ExpandedTerritoryKeysMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Territories",
                columns: table => new
                {
                    TerritoryId = table.Column<Guid>(nullable: false),
                    ClanWarId = table.Column<Guid>(nullable: false),
                    MapId = table.Column<Guid>(nullable: false),
                    MapInstanceId = table.Column<Guid>(nullable: false),
                    GuildId = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territories", x => new { x.TerritoryId, x.ClanWarId, x.MapId, x.MapInstanceId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Territories");
        }
    }
}
