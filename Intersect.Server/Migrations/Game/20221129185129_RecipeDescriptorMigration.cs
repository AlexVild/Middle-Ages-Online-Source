using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class RecipeDescriptorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeCreated = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Hint = table.Column<string>(nullable: true),
                    CraftType = table.Column<int>(nullable: false),
                    Trigger = table.Column<int>(nullable: false),
                    TriggerParam = table.Column<Guid>(nullable: false),
                    Requirements = table.Column<string>(nullable: true),
                    Folder = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    HiddenUntilUnlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
