using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class ChallengeContractMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractRequirements",
                table: "Challenges",
                defaultValue: "[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "RequirementsString",
                table: "Challenges",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresContract",
                table: "Challenges",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractRequirements",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "RequirementsString",
                table: "Challenges");

            migrationBuilder.DropColumn(
                name: "RequiresContract",
                table: "Challenges");
        }
    }
}
