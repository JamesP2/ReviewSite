namespace Review_Site.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CategoryID = c.Guid(nullable: false),
                        AuthorID = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        ShortDescription = c.String(maxLength: 150),
                        Text = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(nullable: false),
                        Issue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.AuthorID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.AuthorID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Username = c.String(),
                        Password = c.Binary(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AuthWithAD = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreatorID = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Type = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: true)
                .Index(t => t.CreatorID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ColorID = c.Guid(nullable: false),
                        GridID = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        IsSystemCategory = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Grids", t => t.GridID, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.ColorID, cascadeDelete: true)
                .Index(t => t.GridID)
                .Index(t => t.ColorID);
            
            CreateTable(
                "dbo.Grids",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Alias = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GridElements",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        BorderColorID = c.Guid(nullable: false),
                        ArticleID = c.Guid(nullable: false),
                        GridID = c.Guid(nullable: false),
                        ImageID = c.Guid(nullable: false),
                        SizeClass = c.String(nullable: false),
                        HeadingClass = c.String(nullable: false),
                        HeadingText = c.String(nullable: false),
                        Width = c.Int(nullable: false),
                        UseHeadingText = c.Boolean(nullable: false),
                        InverseHeading = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Colors", t => t.BorderColorID, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.ArticleID)
                .ForeignKey("dbo.Grids", t => t.GridID, cascadeDelete: true)
                .ForeignKey("dbo.Resources", t => t.ImageID, cascadeDelete: true)
                .Index(t => t.BorderColorID)
                .Index(t => t.ArticleID)
                .Index(t => t.GridID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.GridElements", new[] { "ImageID" });
            DropIndex("dbo.GridElements", new[] { "GridID" });
            DropIndex("dbo.GridElements", new[] { "ArticleID" });
            DropIndex("dbo.GridElements", new[] { "BorderColorID" });
            DropIndex("dbo.Categories", new[] { "ColorID" });
            DropIndex("dbo.Categories", new[] { "GridID" });
            DropIndex("dbo.Resources", new[] { "CreatorID" });
            DropIndex("dbo.Articles", new[] { "CategoryID" });
            DropIndex("dbo.Articles", new[] { "AuthorID" });
            DropForeignKey("dbo.GridElements", "ImageID", "dbo.Resources");
            DropForeignKey("dbo.GridElements", "GridID", "dbo.Grids");
            DropForeignKey("dbo.GridElements", "ArticleID", "dbo.Articles");
            DropForeignKey("dbo.GridElements", "BorderColorID", "dbo.Colors");
            DropForeignKey("dbo.Categories", "ColorID", "dbo.Colors");
            DropForeignKey("dbo.Categories", "GridID", "dbo.Grids");
            DropForeignKey("dbo.Resources", "CreatorID", "dbo.Users");
            DropForeignKey("dbo.Articles", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Articles", "AuthorID", "dbo.Users");
            DropTable("dbo.Colors");
            DropTable("dbo.GridElements");
            DropTable("dbo.Grids");
            DropTable("dbo.Categories");
            DropTable("dbo.Resources");
            DropTable("dbo.Users");
            DropTable("dbo.Articles");
        }
    }
}
