namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Long(nullable: false, identity: true),
                        Content = c.String(maxLength: 500),
                        Lession_LesionId = c.Long(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Lessions", t => t.Lession_LesionId)
                .Index(t => t.Lession_LesionId);
            
            CreateTable(
                "dbo.Lessions",
                c => new
                    {
                        LesionId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        YoutubeLink = c.String(nullable: false),
                        Course_CourseId = c.Long(),
                    })
                .PrimaryKey(t => t.LesionId)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId)
                .Index(t => t.Course_CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Introduction = c.String(nullable: false),
                        TuitionFee = c.Double(nullable: false),
                        EntryTestXml = c.String(nullable: false),
                        TimeLimit = c.Long(nullable: false),
                        PassMark = c.Long(nullable: false),
                        ImageLink = c.String(nullable: false),
                        Platform_PlatformId = c.Long(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Platforms", t => t.Platform_PlatformId)
                .Index(t => t.Platform_PlatformId);
            
            CreateTable(
                "dbo.Enrolments",
                c => new
                    {
                        EnrolmentId = c.Long(nullable: false, identity: true),
                        CreateDate = c.String(nullable: false),
                        EndDate = c.String(nullable: false),
                        IsValid = c.Boolean(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Course_CourseId = c.Long(),
                    })
                .PrimaryKey(t => t.EnrolmentId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId)
                .Index(t => t.User_Id)
                .Index(t => t.Course_CourseId);
            
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
                        Content = c.String(nullable: false),
                        Point = c.Long(nullable: false),
                        Lession_LesionId = c.Long(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Lessions", t => t.Lession_LesionId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Lession_LesionId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        XmlContent = c.String(nullable: false),
                        TimeLimit = c.Long(nullable: false),
                        PassMark = c.Long(nullable: false),
                        Lession_LesionId = c.Long(),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.Lessions", t => t.Lession_LesionId)
                .Index(t => t.Lession_LesionId);
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        TestResultId = c.Long(nullable: false, identity: true),
                        Mark = c.Long(nullable: false),
                        Passed = c.Boolean(nullable: false),
                        Test_TestId = c.Long(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.TestResultId)
                .ForeignKey("dbo.Tests", t => t.Test_TestId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Test_TestId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        EmailId = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        HtmlContent = c.String(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        EmailTemplateId = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        HtmlContent = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmailTemplateId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Emails", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TestResults", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TestResults", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.Tests", "Lession_LesionId", "dbo.Lessions");
            DropForeignKey("dbo.Ratings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "Lession_LesionId", "dbo.Lessions");
            DropForeignKey("dbo.Courses", "Platform_PlatformId", "dbo.Platforms");
            DropForeignKey("dbo.Lessions", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.Enrolments", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.Enrolments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Lession_LesionId", "dbo.Lessions");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Emails", new[] { "User_Id" });
            DropIndex("dbo.TestResults", new[] { "User_Id" });
            DropIndex("dbo.TestResults", new[] { "Test_TestId" });
            DropIndex("dbo.Tests", new[] { "Lession_LesionId" });
            DropIndex("dbo.Ratings", new[] { "User_Id" });
            DropIndex("dbo.Ratings", new[] { "Lession_LesionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Enrolments", new[] { "Course_CourseId" });
            DropIndex("dbo.Enrolments", new[] { "User_Id" });
            DropIndex("dbo.Courses", new[] { "Platform_PlatformId" });
            DropIndex("dbo.Lessions", new[] { "Course_CourseId" });
            DropIndex("dbo.Comments", new[] { "Lession_LesionId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EmailTemplates");
            DropTable("dbo.Emails");
            DropTable("dbo.TestResults");
            DropTable("dbo.Tests");
            DropTable("dbo.Ratings");
            DropTable("dbo.Platforms");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Enrolments");
            DropTable("dbo.Courses");
            DropTable("dbo.Lessions");
            DropTable("dbo.Comments");
        }
    }
}
