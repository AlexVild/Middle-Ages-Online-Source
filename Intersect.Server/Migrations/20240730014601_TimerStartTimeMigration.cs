using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class TimerStartTimeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StartTime",
                table: "Timers",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Timers");
        }
    }
}
