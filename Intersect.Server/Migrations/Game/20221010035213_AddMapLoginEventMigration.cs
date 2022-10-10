using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations.Game
{
    public partial class AddMapLoginEventMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LoginEvent",
                table: "Maps",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginEvent",
                table: "Maps");
        }
    }
}
