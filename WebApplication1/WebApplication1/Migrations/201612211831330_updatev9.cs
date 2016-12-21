namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatev9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tutorials",
                c => new
                    {
                        TutorialId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.TutorialId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tutorials");
        }
    }
}
