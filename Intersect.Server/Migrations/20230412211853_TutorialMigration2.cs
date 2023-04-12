using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class TutorialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CosmeticsTutorialDone",
                table: "Players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LabelTutorialDone",
                table: "Players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RecipeTutorialDone",
                table: "Players",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CosmeticsTutorialDone",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LabelTutorialDone",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RecipeTutorialDone",
                table: "Players");
        }
    }
}
