using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class ShopImprovementMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "BuyMultiplier",
                table: "Shops",
                nullable: false,
                defaultValue: 1.0f);

            migrationBuilder.AddColumn<string>(
                name: "BuyingTags",
                table: "Shops",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<bool>(
                name: "TagWhitelist",
                table: "Shops",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyMultiplier",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "BuyingTags",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "TagWhitelist",
                table: "Shops");
        }
    }
}
