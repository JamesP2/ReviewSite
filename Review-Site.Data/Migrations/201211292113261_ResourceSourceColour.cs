namespace Review_Site.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResourceSourceColour : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resources", "SourceTextColorID", c => c.Guid());
            AddForeignKey("dbo.Resources", "SourceTextColorID", "dbo.Colors", "ID");
            CreateIndex("dbo.Resources", "SourceTextColorID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Resources", new[] { "SourceTextColorID" });
            DropForeignKey("dbo.Resources", "SourceTextColorID", "dbo.Colors");
            DropColumn("dbo.Resources", "SourceTextColorID");
        }
    }
}
