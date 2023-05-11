using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class OnHitReduxMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Combat_PersistMissedAttack",
                table: "Spells",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Combat_PersistWeaponSwap",
                table: "Spells",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Combat_PersistMissedAttack",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "Combat_PersistWeaponSwap",
                table: "Spells");
        }
    }
}
