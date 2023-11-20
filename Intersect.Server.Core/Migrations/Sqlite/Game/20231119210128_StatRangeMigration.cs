using System;
using Intersect.Server.Database.PlayerData;
using Intersect.Server.Database;
using Microsoft.EntityFrameworkCore.Migrations;
using SqlKata;
using Intersect.Server.Database.GameData;
using SqlKata.Extensions;
using SqlKata.Compilers;
using Intersect.Server.Migrations.SqlGeneration;

#nullable disable

namespace Intersect.Server.Migrations.Sqlite.Game
{
    /// <inheritdoc />
    public partial class StatRangeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items_EquipmentProperties",
                columns: table => new
                {
                    DescriptorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StatRange_Attack_LowRange = table.Column<int>(type: "INTEGER", nullable: true),
                    StatRange_Attack_HighRange = table.Column<int>(type: "INTEGER", nullable: true),
                    StatRange_AbilityPower_LowRange = table.Column<int>(type: "INTEGER", nullable: true),
                    StatRange_AbilityPower_HighRange = table.Column<int>(type: "INTEGER", nullable: true),
                    StatRange_Defense_LowRange = table.Column<int>(type: "INTEGER", nullable: true),
                    StatRange_Defense_HighRange = table.Column<int>(type: "INTEGER", nullable: true),
                    StatRange_MagicResist_LowRange = table.Column<int>(type: "INTEGER", nullable: true),
                    StatRange_MagicResist_HighRange = table.Column<int>(type: "INTEGER", nullable: true),
                    StatRange_Speed_LowRange = table.Column<int>(type: "INTEGER", nullable: true),
                    StatRange_Speed_HighRange = table.Column<int>(type: "INTEGER", nullable: true),
                    Seed = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items_EquipmentProperties", x => x.DescriptorId);
                    table.ForeignKey(
                        name: "FK_Items_EquipmentProperties_Items_DescriptorId",
                        column: x => x.DescriptorId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql(new StatRangeMigrationSql().BuildSqliteSql());

            migrationBuilder.DropColumn(
                name: "StatGrowth",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items_EquipmentProperties");

            migrationBuilder.AddColumn<int>(
                name: "StatGrowth",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
