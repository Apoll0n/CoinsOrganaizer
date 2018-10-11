using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer2.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    CoinForeignKey = table.Column<int>(nullable: false),
                    NickName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    SalePrice = table.Column<double>(nullable: false),
                    TrackNumber = table.Column<string>(nullable: true),
                    OrderDetails = table.Column<string>(nullable: true),
                    WhereSold = table.Column<string>(nullable: false),
                    SaleCurrency = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: false),
                    CoinId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    CoinId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    Link = table.Column<string>(nullable: false),
                    PolishName = table.Column<string>(nullable: true),
                    EnglishName = table.Column<string>(nullable: true),
                    ZlotyPrice = table.Column<double>(nullable: false),
                    DollarPrice = table.Column<double>(nullable: false),
                    IsSold = table.Column<bool>(nullable: false),
                    IsInStock = table.Column<bool>(nullable: false),
                    AversFotoLink = table.Column<string>(nullable: true),
                    ReversFotoLink = table.Column<string>(nullable: true),
                    OrderForeignKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.CoinId);
                    table.ForeignKey(
                        name: "FK_Coins_Orders_OrderForeignKey",
                        column: x => x.OrderForeignKey,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coins_OrderForeignKey",
                table: "Coins",
                column: "OrderForeignKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
