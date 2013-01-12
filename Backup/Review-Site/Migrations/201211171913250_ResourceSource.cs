namespace Review_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResourceSource : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resources", "Source", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resources", "Source");
        }
    }
}
