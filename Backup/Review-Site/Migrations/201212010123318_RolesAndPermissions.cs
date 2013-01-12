namespace Review_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesAndPermissions : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserRoles", new[] { "Role_ID" });
            DropIndex("dbo.UserRoles", new[] { "User_ID" });
            DropIndex("dbo.RolePermissions", new[] { "Permission_ID" });
            DropIndex("dbo.RolePermissions", new[] { "Role_ID" });
            DropForeignKey("dbo.UserRoles", "Role_ID", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_ID", "dbo.Users");
            DropForeignKey("dbo.RolePermissions", "Permission_ID", "dbo.Permissions");
            DropForeignKey("dbo.RolePermissions", "Role_ID", "dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Permissions");
            DropTable("dbo.Roles");
        }
    }
}
