namespace Review_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
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
                        LastModified = c.DateTime(),
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
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreatorID = c.Guid(nullable: false),
                        SourceTextColorID = c.Guid(),
                        Title = c.String(nullable: false),
                        Type = c.String(),
                        Source = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.CreatorID, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.SourceTextColorID)
                .Index(t => t.CreatorID)
                .Index(t => t.SourceTextColorID);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Identifier = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ColorID = c.Guid(nullable: false),
                        GridID = c.Guid(),
                        Title = c.String(nullable: false),
                        IsSystemCategory = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Grids", t => t.GridID)
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
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
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
                        HeadingText = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
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
                "dbo.Tags",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PageHits",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Target = c.Guid(nullable: false),
                        Time = c.DateTime(nullable: false),
                        ClientAddress = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Role_ID = c.Guid(nullable: false),
                        Permission_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_ID, t.Permission_ID })
                .ForeignKey("dbo.Roles", t => t.Role_ID, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.Permission_ID, cascadeDelete: true)
                .Index(t => t.Role_ID)
                .Index(t => t.Permission_ID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_ID = c.Guid(nullable: false),
                        Role_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_ID, t.Role_ID })
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.User_ID)
                .Index(t => t.Role_ID);
            
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
            DropForeignKey("dbo.Articles", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ColorID", "dbo.Colors");
            DropForeignKey("dbo.Categories", "GridID", "dbo.Grids");
            DropForeignKey("dbo.GridElements", "ImageID", "dbo.Resources");
            DropForeignKey("dbo.GridElements", "GridID", "dbo.Grids");
            DropForeignKey("dbo.GridElements", "ArticleID", "dbo.Articles");
            DropForeignKey("dbo.GridElements", "BorderColorID", "dbo.Colors");
            DropForeignKey("dbo.Articles", "AuthorID", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "Role_ID", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_ID", "dbo.Users");
            DropForeignKey("dbo.RolePermissions", "Permission_ID", "dbo.Permissions");
            DropForeignKey("dbo.RolePermissions", "Role_ID", "dbo.Roles");
            DropForeignKey("dbo.Resources", "SourceTextColorID", "dbo.Colors");
            DropForeignKey("dbo.Resources", "CreatorID", "dbo.Users");
            DropIndex("dbo.ArticleTags", new[] { "Tag_ID" });
            DropIndex("dbo.ArticleTags", new[] { "Article_ID" });
            DropIndex("dbo.Articles", new[] { "CategoryID" });
            DropIndex("dbo.Categories", new[] { "ColorID" });
            DropIndex("dbo.Categories", new[] { "GridID" });
            DropIndex("dbo.GridElements", new[] { "ImageID" });
            DropIndex("dbo.GridElements", new[] { "GridID" });
            DropIndex("dbo.GridElements", new[] { "ArticleID" });
            DropIndex("dbo.GridElements", new[] { "BorderColorID" });
            DropIndex("dbo.Articles", new[] { "AuthorID" });
            DropIndex("dbo.UserRoles", new[] { "Role_ID" });
            DropIndex("dbo.UserRoles", new[] { "User_ID" });
            DropIndex("dbo.RolePermissions", new[] { "Permission_ID" });
            DropIndex("dbo.RolePermissions", new[] { "Role_ID" });
            DropIndex("dbo.Resources", new[] { "SourceTextColorID" });
            DropIndex("dbo.Resources", new[] { "CreatorID" });
            DropTable("dbo.ArticleTags");
            DropTable("dbo.UserRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.PageHits");
            DropTable("dbo.Tags");
            DropTable("dbo.GridElements");
            DropTable("dbo.Grids");
            DropTable("dbo.Categories");
            DropTable("dbo.Permissions");
            DropTable("dbo.Roles");
            DropTable("dbo.Colors");
            DropTable("dbo.Resources");
            DropTable("dbo.Users");
            DropTable("dbo.Articles");
        }
    }
}
