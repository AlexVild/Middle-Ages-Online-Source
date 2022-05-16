using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class TimerMigrationNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ContinueAfterExpiration",
                table: "Timers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LogoutBehavior",
                table: "Timers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoutBehavior",
                table: "Timers");

            migrationBuilder.DropColumn(
                name: "ContinueAfterExpiration",
                table: "Timers");
        }
    }
}
