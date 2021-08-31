using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class ComboEquipmentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComboSpell",
                table: "Items",
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
