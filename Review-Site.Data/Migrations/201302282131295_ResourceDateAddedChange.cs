namespace Review_Site.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResourceDateAddedChange : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Resources", "DateAdded", "Created");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Resources", "Created", "DateAdded");
        }
    }
}
