using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Initial11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coins",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "CoinCost",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "SoldPrice",
                table: "Coins");

            migrationBuilder.RenameColumn(
                name: "CoinId",
                table: "Orders",
                newName: "CoinForeignKey");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Coins",
                newName: "OrderForeignKey");

            migrationBuilder.AlterColumn<int>(
                name: "CoinId",
                table: "Coins",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coins",
                table: "Coins",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CoinForeignKey",
                table: "Orders",
                column: "CoinForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_OrderForeignKey",
                table: "Coins",
                column: "OrderForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Coins_Orders_OrderForeignKey",
                table: "Coins",
                column: "OrderForeignKey",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Coins_CoinForeignKey",
                table: "Orders",
                column: "CoinForeignKey",
                principalTable: "Coins",
                principalColumn: "CoinId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coins_Orders_OrderForeignKey",
                table: "Coins");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Coins_CoinForeignKey",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CoinForeignKey",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coins",
                table: "Coins");

            migrationBuilder.DropIndex(
                name: "IX_Coins_OrderForeignKey",
                table: "Coins");

            migrationBuilder.RenameColumn(
                name: "CoinForeignKey",
                table: "Orders",
                newName: "CoinId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OrderForeignKey",
                table: "Coins",
                newName: "OrderId");

            migrationBuilder.AddColumn<double>(
                name: "CoinCost",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "CoinId",
                table: "Coins",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Coins",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<double>(
                name: "SalePrice",
                table: "Coins",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SoldPrice",
                table: "Coins",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coins",
                table: "Coins",
                column: "Id");
        }
    }
}
