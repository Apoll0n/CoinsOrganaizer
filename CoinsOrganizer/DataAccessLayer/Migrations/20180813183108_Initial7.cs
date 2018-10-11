using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Initial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NickName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WhereSold",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "DollarPrice",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "IsInStock",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "SoldPrice",
                table: "Coins");

            migrationBuilder.RenameColumn(
                name: "TrackNumber",
                table: "Orders",
                newName: "ReversFotoLink");

            migrationBuilder.RenameColumn(
                name: "SaleCurrency",
                table: "Orders",
                newName: "PolishName");

            migrationBuilder.RenameColumn(
                name: "OrderDetails",
                table: "Orders",
                newName: "EnglishName");

            migrationBuilder.RenameColumn(
                name: "IsPaid",
                table: "Orders",
                newName: "IsSold");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Orders",
                newName: "AversFotoLink");

            migrationBuilder.RenameColumn(
                name: "CoinCost",
                table: "Orders",
                newName: "ZlotyPrice");

            migrationBuilder.RenameColumn(
                name: "ZlotyPrice",
                table: "Coins",
                newName: "CoinCost");

            migrationBuilder.RenameColumn(
                name: "ReversFotoLink",
                table: "Coins",
                newName: "TrackNumber");

            migrationBuilder.RenameColumn(
                name: "PolishName",
                table: "Coins",
                newName: "SaleCurrency");

            migrationBuilder.RenameColumn(
                name: "IsSold",
                table: "Coins",
                newName: "IsPaid");

            migrationBuilder.RenameColumn(
                name: "EnglishName",
                table: "Coins",
                newName: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "AversFotoLink",
                table: "Coins",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DollarPrice",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsInStock",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SoldPrice",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "Coins",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "Coins",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhereSold",
                table: "Coins",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DollarPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsInStock",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SoldPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "WhereSold",
                table: "Coins");

            migrationBuilder.RenameColumn(
                name: "ZlotyPrice",
                table: "Orders",
                newName: "CoinCost");

            migrationBuilder.RenameColumn(
                name: "ReversFotoLink",
                table: "Orders",
                newName: "TrackNumber");

            migrationBuilder.RenameColumn(
                name: "PolishName",
                table: "Orders",
                newName: "SaleCurrency");

            migrationBuilder.RenameColumn(
                name: "IsSold",
                table: "Orders",
                newName: "IsPaid");

            migrationBuilder.RenameColumn(
                name: "EnglishName",
                table: "Orders",
                newName: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "AversFotoLink",
                table: "Orders",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "TrackNumber",
                table: "Coins",
                newName: "ReversFotoLink");

            migrationBuilder.RenameColumn(
                name: "SaleCurrency",
                table: "Coins",
                newName: "PolishName");

            migrationBuilder.RenameColumn(
                name: "OrderDetails",
                table: "Coins",
                newName: "EnglishName");

            migrationBuilder.RenameColumn(
                name: "IsPaid",
                table: "Coins",
                newName: "IsSold");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Coins",
                newName: "AversFotoLink");

            migrationBuilder.RenameColumn(
                name: "CoinCost",
                table: "Coins",
                newName: "ZlotyPrice");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WhereSold",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                table: "Coins",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Coins",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DollarPrice",
                table: "Coins",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsInStock",
                table: "Coins",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Coins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SoldPrice",
                table: "Coins",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
