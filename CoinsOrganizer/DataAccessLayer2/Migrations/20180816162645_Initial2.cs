using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer2.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coins",
                table: "Coins");

            migrationBuilder.DropIndex(
                name: "IX_Coins_OrderForeignKey",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "CoinId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CoinId",
                table: "Coins");

            migrationBuilder.AddColumn<int>(
                name: "CoinerId",
                table: "Coins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coins",
                table: "Coins",
                column: "CoinerId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_OrderForeignKey",
                table: "Coins",
                column: "OrderForeignKey",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coins",
                table: "Coins");

            migrationBuilder.DropIndex(
                name: "IX_Coins_OrderForeignKey",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "CoinerId",
                table: "Coins");

            migrationBuilder.AddColumn<int>(
                name: "CoinId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoinId",
                table: "Coins",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coins",
                table: "Coins",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_OrderForeignKey",
                table: "Coins",
                column: "OrderForeignKey");
        }
    }
}
