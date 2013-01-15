namespace Review_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageHits : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PageHits");
        }
    }
}
