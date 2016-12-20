namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Long(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        LessionId = c.Long(nullable: false),
                        Content = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Lessions", t => t.LessionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LessionId);
            
            CreateTable(
                "dbo.Lessions",
                c => new
                    {
                        LesionId = c.Long(nullable: false, identity: true),
                        CourseId = c.Long(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        YoutubeLink = c.String(),
                    })
                .PrimaryKey(t => t.LesionId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Intrduction = c.String(),
                        TuitionFee = c.Double(nullable: false),
                        EntryTestXml = c.String(),
                        TimeLimit = c.Long(nullable: false),
                        PassMark = c.Long(nullable: false),
                        PlatformId = c.Long(nullable: false),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Platforms", t => t.PlatformId, cascadeDelete: true)
                .Index(t => t.PlatformId);
            
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        PlatformId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PlatformId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Long(nullable: false, identity: true),
                        LessionId = c.Long(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Content = c.String(),
                        Point = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Lessions", t => t.LessionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.LessionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        HomeTown = c.String(),
                        BirthDate = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Long(nullable: false, identity: true),
                        LessionId = c.Long(nullable: false),
                        Name = c.String(),
                        XmlContent = c.String(),
                        TimeLimit = c.Long(nullable: false),
                        PassMark = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.Lessions", t => t.LessionId, cascadeDelete: true)
                .Index(t => t.LessionId);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        EmailId = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        HtmlContent = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        EmailTemplateId = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        HtmlContent = c.String(),
                    })
                .PrimaryKey(t => t.EmailTemplateId);
            
            CreateTable(
                "dbo.Enrolments",
                c => new
                    {
                        EnrolmentId = c.Long(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CourseId = c.Long(nullable: false),
                        CreateDate = c.String(),
                        EndDate = c.String(),
                        IsValid = c.String(),
                    })
                .PrimaryKey(t => t.EnrolmentId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        TestResultId = c.Long(nullable: false, identity: true),
                        TestId = c.Long(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Mark = c.Long(nullable: false),
                        Passed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TestResultId)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TestId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestResults", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TestResults", "TestId", "dbo.Tests");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Enrolments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enrolments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Emails", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "LessionId", "dbo.Lessions");
            DropForeignKey("dbo.Tests", "LessionId", "dbo.Lessions");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "LessionId", "dbo.Lessions");
            DropForeignKey("dbo.Lessions", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "PlatformId", "dbo.Platforms");
            DropIndex("dbo.TestResults", new[] { "UserId" });
            DropIndex("dbo.TestResults", new[] { "TestId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Enrolments", new[] { "CourseId" });
            DropIndex("dbo.Enrolments", new[] { "UserId" });
            DropIndex("dbo.Emails", new[] { "UserId" });
            DropIndex("dbo.Tests", new[] { "LessionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.Ratings", new[] { "LessionId" });
            DropIndex("dbo.Courses", new[] { "PlatformId" });
            DropIndex("dbo.Lessions", new[] { "CourseId" });
            DropIndex("dbo.Comments", new[] { "LessionId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.TestResults");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Enrolments");
            DropTable("dbo.EmailTemplates");
            DropTable("dbo.Emails");
            DropTable("dbo.Tests");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Ratings");
            DropTable("dbo.Platforms");
            DropTable("dbo.Courses");
            DropTable("dbo.Lessions");
            DropTable("dbo.Comments");
        }
    }
}
