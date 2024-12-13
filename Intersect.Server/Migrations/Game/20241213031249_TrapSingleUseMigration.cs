using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class TrapSingleUseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Combat_TrapDamageCooldown",
                table: "Spells",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Combat_TrapSingleUse",
                table: "Spells",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Combat_TrapDamageCooldown",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Combat_TrapSingleUse",
                table: "Spells");
        }
    }
}
