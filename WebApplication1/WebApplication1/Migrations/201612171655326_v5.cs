namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessions", "ImageLink", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lessions", "ImageLink");
        }
    }
}
