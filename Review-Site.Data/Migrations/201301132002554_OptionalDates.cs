namespace Review_Site.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Created", c => c.DateTime());
            AddColumn("dbo.Users", "LastModified", c => c.DateTime());
            AddColumn("dbo.Resources", "LastModified", c => c.DateTime());
            AddColumn("dbo.Colors", "Created", c => c.DateTime());
            AddColumn("dbo.Colors", "LastModified", c => c.DateTime());
            AddColumn("dbo.Categories", "Created", c => c.DateTime());
            AddColumn("dbo.Categories", "LastModified", c => c.DateTime());
            AddColumn("dbo.Grids", "Created", c => c.DateTime());
            AddColumn("dbo.Grids", "LastModified", c => c.DateTime());
            AddColumn("dbo.GridElements", "Created", c => c.DateTime());
            AddColumn("dbo.GridElements", "LastModified", c => c.DateTime());
            AlterColumn("dbo.Articles", "Created", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Created", c => c.DateTime(nullable: false));
            DropColumn("dbo.GridElements", "LastModified");
            DropColumn("dbo.GridElements", "Created");
            DropColumn("dbo.Grids", "LastModified");
            DropColumn("dbo.Grids", "Created");
            DropColumn("dbo.Categories", "LastModified");
            DropColumn("dbo.Categories", "Created");
            DropColumn("dbo.Colors", "LastModified");
            DropColumn("dbo.Colors", "Created");
            DropColumn("dbo.Resources", "LastModified");
            DropColumn("dbo.Users", "LastModified");
            DropColumn("dbo.Users", "Created");
        }
    }
}
