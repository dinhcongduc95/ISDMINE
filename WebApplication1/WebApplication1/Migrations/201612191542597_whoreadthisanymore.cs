namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whoreadthisanymore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Level", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Level");
        }
    }
}
