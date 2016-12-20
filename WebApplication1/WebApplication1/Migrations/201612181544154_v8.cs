namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestResults", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TestResults", new[] { "User_Id" });
            AlterColumn("dbo.TestResults", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.TestResults", "User_Id");
            AddForeignKey("dbo.TestResults", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TestResults", new[] { "User_Id" });
            AlterColumn("dbo.TestResults", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TestResults", "User_Id");
            AddForeignKey("dbo.TestResults", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
