using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Brorep.Persistence.Migrations
{
    public partial class JudgingTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JudgingTypes",
                columns: table => new
                {
                    JudgingTypeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JudgingTypes", x => x.JudgingTypeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JudgingTypes");
        }
    }
}
