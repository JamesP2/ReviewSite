namespace Review_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HeadingTextNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GridElements", "HeadingText", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GridElements", "HeadingText", c => c.String(nullable: false));
        }
    }
}
