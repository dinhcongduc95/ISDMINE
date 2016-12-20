namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "Lession_LesionId", "dbo.Lessions");
            DropForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "Lession_LesionId" });
            DropIndex("dbo.Ratings", new[] { "User_Id" });
            AddColumn("dbo.Comments", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "User_Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.Ratings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Long(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Point = c.Long(nullable: false),
                        Lession_LesionId = c.Long(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RatingId);
            
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropColumn("dbo.Comments", "User_Id");
            CreateIndex("dbo.Ratings", "User_Id");
            CreateIndex("dbo.Ratings", "Lession_LesionId");
            AddForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ratings", "Lession_LesionId", "dbo.Lessions", "LesionId");
        }
    }
}
