namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        PlatformId = c.Long(nullable: false),
                        Introduction = c.String(),
                        TuitionFee = c.Long(nullable: false),
                        EntryTestXml = c.String(),
                        TimeLimit = c.Int(nullable: false),
                        PassMark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Courses");
        }
    }
}
