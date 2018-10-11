using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "WhereSold",
                table: "Coins");

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

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    CoinId = table.Column<int>(nullable: false),
                    NickName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    SalePrice = table.Column<double>(nullable: false),
                    TrackNumber = table.Column<string>(nullable: true),
                    OrderDetails = table.Column<string>(nullable: true),
                    CoinCost = table.Column<double>(nullable: false),
                    WhereSold = table.Column<string>(nullable: false),
                    SaleCurrency = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Course");

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

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AversFotoLink = table.Column<string>(nullable: true),
                    CoinId = table.Column<int>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    DollarPrice = table.Column<double>(nullable: false),
                    EnglishName = table.Column<string>(nullable: true),
                    IsInStock = table.Column<bool>(nullable: false),
                    IsSold = table.Column<bool>(nullable: false),
                    Link = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    PolishName = table.Column<string>(nullable: true),
                    ReversFotoLink = table.Column<string>(nullable: true),
                    SalePrice = table.Column<double>(nullable: false),
                    SoldPrice = table.Column<double>(nullable: false),
                    ZlotyPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }
    }
}
