using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class EnhancementWeaponTypesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumWeaponLevel",
                table: "Enhancements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ValidWeaponTypes",
                table: "Enhancements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumWeaponLevel",
                table: "Enhancements");

            migrationBuilder.DropColumn(
                name: "ValidWeaponTypes",
                table: "Enhancements");
        }
    }
}
