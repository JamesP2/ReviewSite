namespace Review_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixColor : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Colors", "LastModified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Colors", "LastModified", c => c.DateTime());
        }
    }
}
