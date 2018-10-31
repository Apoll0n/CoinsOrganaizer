namespace CoinsOrganizerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCollection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coins",
                c => new
                    {
                        CoinId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Cost = c.Double(nullable: false),
                        Link = c.String(nullable: false),
                        PolishName = c.String(),
                        EnglishName = c.String(),
                        ZlotyPrice = c.Double(nullable: false),
                        DollarPrice = c.Double(nullable: false),
                        IsSold = c.Boolean(nullable: false),
                        IsInStock = c.Boolean(nullable: false),
                        AversFotoLink = c.String(),
                        ReversFotoLink = c.String(),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CoinId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        NickName = c.String(nullable: false),
                        Email = c.String(),
                        SalePrice = c.Double(nullable: false),
                        TrackNumber = c.String(),
                        OrderDetails = c.String(),
                        WhereSold = c.String(nullable: false),
                        SaleCurrency = c.String(),
                        Link = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                        CoinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Coins", t => t.CoinId, cascadeDelete: true)
                .Index(t => t.CoinId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CoinId", "dbo.Coins");
            DropIndex("dbo.Orders", new[] { "CoinId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Coins");
        }
    }
}
