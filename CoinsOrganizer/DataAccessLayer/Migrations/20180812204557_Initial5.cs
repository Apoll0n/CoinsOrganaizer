using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Coins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SalePrice",
                table: "Coins",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Orders",
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
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Coins");
        }
    }
}
