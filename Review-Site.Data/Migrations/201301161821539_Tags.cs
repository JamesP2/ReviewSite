namespace Review_Site.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ArticleTags",
                c => new
                    {
                        Article_ID = c.Guid(nullable: false),
                        Tag_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Article_ID, t.Tag_ID })
                .ForeignKey("dbo.Articles", t => t.Article_ID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .Index(t => t.Article_ID)
                .Index(t => t.Tag_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleTags", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.ArticleTags", "Article_ID", "dbo.Articles");
            DropIndex("dbo.ArticleTags", new[] { "Tag_ID" });
            DropIndex("dbo.ArticleTags", new[] { "Article_ID" });
            DropTable("dbo.ArticleTags");
            DropTable("dbo.Tags");
        }
    }
}
