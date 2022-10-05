using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class AddLabelOptionsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Labels",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MatchNameColor",
                table: "Labels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Labels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "MatchNameColor",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Labels");
        }
    }
}
