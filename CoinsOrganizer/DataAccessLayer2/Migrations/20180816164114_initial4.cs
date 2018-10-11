using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer2.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinForeignKey",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoinForeignKey",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }
    }
}
