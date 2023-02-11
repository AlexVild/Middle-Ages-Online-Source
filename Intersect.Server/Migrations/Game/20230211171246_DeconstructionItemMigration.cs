using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class DeconstructionItemMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CraftWeaponExp",
                table: "Items",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "EnhancementThreshold",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fuel",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuelRequired",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DeconstructRolls",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CraftWeaponExp",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EnhancementThreshold",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Fuel",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FuelRequired",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DeconstructRolls",
                table: "Items");
        }
    }
}
