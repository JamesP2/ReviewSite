namespace Review_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "GridID", "dbo.Grids");
            DropIndex("dbo.Categories", new[] { "GridID" });
            AlterColumn("dbo.Categories", "GridID", c => c.Guid());
            AddForeignKey("dbo.Categories", "GridID", "dbo.Grids", "ID");
            CreateIndex("dbo.Categories", "GridID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categories", new[] { "GridID" });
            DropForeignKey("dbo.Categories", "GridID", "dbo.Grids");
            AlterColumn("dbo.Categories", "GridID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Categories", "GridID");
            AddForeignKey("dbo.Categories", "GridID", "dbo.Grids", "ID", cascadeDelete: true);
        }
    }
}
