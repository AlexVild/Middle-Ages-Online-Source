using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class AddVehicleColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InVehicle",
                table: "Players",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<string>(
                name: "VehicleSprite",
                table: "Players",
                nullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<long>(
                name: "VehicleSpeed",
                table: "Players",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
