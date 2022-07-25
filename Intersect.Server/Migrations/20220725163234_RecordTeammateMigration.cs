using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class RecordTeammateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Record_Teammate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordInstanceId = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record_Teammate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Record_Teammate_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Record_Teammate_Player_Record_RecordInstanceId",
                        column: x => x.RecordInstanceId,
                        principalTable: "Player_Record",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Record_Teammate_PlayerId",
                table: "Record_Teammate",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_Teammate_RecordInstanceId",
                table: "Record_Teammate",
                column: "RecordInstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Record_Teammate");
        }
    }
}
