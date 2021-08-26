using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class AnimationBrightnessThreshold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrightnessThreshold",
                table: "Animations",
                nullable: false,
                defaultValue: 100);
        }
    }
}
