namespace CoinsOrganizerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsIgnoredProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coins", "IsIgnored", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Coins", "IsIgnored");
        }
    }
}
