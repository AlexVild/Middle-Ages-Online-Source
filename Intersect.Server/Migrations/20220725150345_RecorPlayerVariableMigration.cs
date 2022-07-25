using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class RecorPlayerVariableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScoreType",
                table: "Player_Record",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreType",
                table: "Player_Record");
        }
    }
}
