using Microsoft.EntityFrameworkCore.Migrations;

namespace Brorep.Persistence.Migrations
{
    public partial class Coins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Coins",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coins",
                table: "AspNetUsers");
        }
    }
}
