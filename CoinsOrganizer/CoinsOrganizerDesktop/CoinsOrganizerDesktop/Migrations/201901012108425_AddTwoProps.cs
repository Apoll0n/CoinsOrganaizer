namespace CoinsOrganizerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTwoProps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsTrackedOnMarket", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "IsShipped", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsShipped");
            DropColumn("dbo.Orders", "IsTrackedOnMarket");
        }
    }
}
