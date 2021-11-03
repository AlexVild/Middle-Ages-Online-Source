using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class DestroyItemMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanDestroy",
                table: "Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CannotDestroyMessage",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestroyRequirements",
                table: "Items",
                nullable: false,
                defaultValue: "[]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanDestroy",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CannotDestroyMessage",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DestroyRequirements",
                table: "Items");
        }
    }
}
