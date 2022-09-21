using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class AdditionalNPCTablesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondaryDrops",
                table: "Npcs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TertiaryDrops",
                table: "Npcs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondaryDrops",
                table: "Npcs");

            migrationBuilder.DropColumn(
                name: "TertiaryDrops",
                table: "Npcs");
        }
    }
}
