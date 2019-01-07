namespace CoinsOrganizerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateTimeProps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PlacedOnMarketDate", c => c.DateTime());
            AddColumn("dbo.Orders", "SoldDate", c => c.DateTime());
            AddColumn("dbo.Orders", "PaidDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PaidDate");
            DropColumn("dbo.Orders", "SoldDate");
            DropColumn("dbo.Orders", "PlacedOnMarketDate");
        }
    }
}
