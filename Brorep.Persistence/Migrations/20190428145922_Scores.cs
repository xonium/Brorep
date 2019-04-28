using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Brorep.Persistence.Migrations
{
    public partial class Scores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    ScoreId = table.Column<Guid>(nullable: false),
                    Points = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    RepId = table.Column<Guid>(nullable: true),
                    ByUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.ScoreId);
                    table.ForeignKey(
                        name: "FK_Scores_AspNetUsers_ByUserId",
                        column: x => x.ByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scores_Reps_RepId",
                        column: x => x.RepId,
                        principalTable: "Reps",
                        principalColumn: "RepId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ByUserId",
                table: "Scores",
                column: "ByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_RepId",
                table: "Scores",
                column: "RepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");
        }
    }
}
