using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class AddPersonalInstanceFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InVehicle",
                table: "Players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "VehicleSpeed",
                table: "Players",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "VehicleSprite",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InVehicle",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "VehicleSpeed",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "VehicleSprite",
                table: "Players");
        }
    }
}
