namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig07 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Banners", "ImageName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Banners", "ImageName", c => c.String(nullable: false, maxLength: 150));
        }
    }
}
