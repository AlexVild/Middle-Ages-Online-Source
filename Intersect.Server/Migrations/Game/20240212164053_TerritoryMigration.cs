using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class TerritoryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Territories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeCreated = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Folder = table.Column<string>(nullable: true),
                    CaptureMs = table.Column<long>(nullable: false),
                    PointsPerTick = table.Column<int>(nullable: false),
                    PointsPerCapture = table.Column<int>(nullable: false),
                    PointsPerDefend = table.Column<int>(nullable: false),
                    PointsPerAttack = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Territories");
        }
    }
}
