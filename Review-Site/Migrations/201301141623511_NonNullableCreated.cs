namespace Review_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data.Entity;
    using Review_Site.Models;
    
    public partial class NonNullableCreated : DbMigration
    {
          public override void Up()
        {
            Sql("UPDATE dbo.Users SET Created = GETDATE() WHERE Created is null", true);
            Sql("UPDATE dbo.Categories SET Created = GETDATE() WHERE Created is null", true);
            Sql("UPDATE dbo.Grids SET Created = GETDATE() WHERE Created is null", true);
            Sql("UPDATE dbo.GridElements SET Created = GETDATE() WHERE Created is null", true);
            AlterColumn("dbo.Users", "Created", c => c.DateTime(nullable: false));
            DropColumn("dbo.Colors", "Created");
            AlterColumn("dbo.Categories", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Grids", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.GridElements", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.Colors", "Created", c => c.DateTime());
            AlterColumn("dbo.GridElements", "Created", c => c.DateTime());
            AlterColumn("dbo.Grids", "Created", c => c.DateTime());
            AlterColumn("dbo.Categories", "Created", c => c.DateTime());
            AlterColumn("dbo.Colors", "Created", c => c.DateTime());
            AlterColumn("dbo.Users", "Created", c => c.DateTime());
        }
    }
}
