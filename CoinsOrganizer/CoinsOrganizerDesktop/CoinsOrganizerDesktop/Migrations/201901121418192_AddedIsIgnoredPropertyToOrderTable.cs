namespace CoinsOrganizerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsIgnoredPropertyToOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsIgnored", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsIgnored");
        }
    }
}
