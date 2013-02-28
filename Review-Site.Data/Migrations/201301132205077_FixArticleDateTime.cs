namespace Review_Site.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixArticleDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Created", c => c.DateTime());
        }
    }
}
