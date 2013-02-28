namespace Review_Site.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleNullableDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "LastModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "LastModified", c => c.DateTime(nullable: false));
        }
    }
}
