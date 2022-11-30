using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class RecipeRequirementsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipeRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Hint = table.Column<string>(nullable: true),
                    Trigger = table.Column<int>(nullable: false),
                    DescriptorId = table.Column<Guid>(nullable: false),
                    TriggerId = table.Column<Guid>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    IsBool = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeRequirements_Recipes_DescriptorId",
                        column: x => x.DescriptorId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRequirements_DescriptorId",
                table: "RecipeRequirements",
                column: "DescriptorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeRequirements");
        }
    }
}
