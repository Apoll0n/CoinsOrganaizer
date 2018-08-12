using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AversFotoLink",
                table: "Coins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReversFotoLink",
                table: "Coins",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AversFotoLink",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "ReversFotoLink",
                table: "Coins");
        }
    }
}
