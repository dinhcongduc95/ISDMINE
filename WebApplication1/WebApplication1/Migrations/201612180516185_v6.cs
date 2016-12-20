namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrolments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Enrolments", new[] { "User_Id" });
            AddColumn("dbo.Comments", "Star", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Enrolments", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Enrolments", "User_Id");
            AddForeignKey("dbo.Enrolments", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrolments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Enrolments", new[] { "User_Id" });
            AlterColumn("dbo.Enrolments", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Comments", "Content", c => c.String(maxLength: 500));
            DropColumn("dbo.Comments", "Star");
            CreateIndex("dbo.Enrolments", "User_Id");
            AddForeignKey("dbo.Enrolments", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
