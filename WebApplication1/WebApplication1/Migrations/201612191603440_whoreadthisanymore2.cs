namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whoreadthisanymore2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Introduction", c => c.String(nullable: false));
            DropColumn("dbo.Courses", "Intrduction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Intrduction", c => c.String(nullable: false));
            DropColumn("dbo.Courses", "Introduction");
        }
    }
}
