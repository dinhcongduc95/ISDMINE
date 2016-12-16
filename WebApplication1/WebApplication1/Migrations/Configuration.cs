using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WebApplication1.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //context.Platforms.AddOrUpdate(
            //    m => m.PlatformId,
            //    new Platform
            //    {
            //        Name = "JAVA",
            //        Description = "Nền tảng Java"
            //    },
            //    new Platform
            //    {
            //        Name = "C#",
            //        Description = "Nền tảng .NET"
            //    },
            //    new Platform
            //    {
            //        Name = "Python",
            //        Description = "Nền tảng Python"
            //    }, new Platform
            //    {
            //        Name = "PHP",
            //        Description = "Nền tảng PHP"
            //    }
            //    );
            //context.Courses.AddOrUpdate(
            //    m => m.Name,
            //    new Course
            //    {
            //        Intrduction = "Đây là khóa học java 1",
            //        EntryTestXml = "Chưa có",
            //        Name = "JAVA1",
            //        TimeLimit = 45,
            //        TuitionFee = 12000,
            //        PassMark = 50,
            //        ImageLink = "~/Content/img/course-img/java.png",
            //        PlatformId = context.Platforms.SingleOrDefault(m => m.Name.Equals("JAVA")).PlatformId
            //    },
            //    new Course
            //    {
            //        Intrduction = "Đây là khóa học java 2",
            //        EntryTestXml = "Chưa có",
            //        Name = "JAVA2",
            //        TimeLimit = 45,
            //        TuitionFee = 12000,
            //        PassMark = 50,
            //        ImageLink = "~/Content/img/course-img/java.png",
            //        PlatformId = context.Platforms.SingleOrDefault(m => m.Name.Equals("JAVA")).PlatformId
            //    },
            //    new Course
            //    {
            //        Intrduction = "Đây là khóa học java 3",
            //        EntryTestXml = "Chưa có",
            //        Name = "JAVA3",
            //        TimeLimit = 45,
            //        TuitionFee = 12000,
            //        PassMark = 50,
            //        ImageLink = "~/Content/img/course-img/java.png",
            //        PlatformId = context.Platforms.SingleOrDefault(m => m.Name.Equals("JAVA")).PlatformId
            //    },
            //    new Course
            //    {
            //        Intrduction = "Đây là khóa học .NET 1",
            //        EntryTestXml = "Chưa có",
            //        Name = ".NET1",
            //        TimeLimit = 45,
            //        TuitionFee = 12000,
            //        PassMark = 50,
            //        ImageLink = "~/Content/img/course-img/c2.png",
            //        PlatformId = context.Platforms.SingleOrDefault(m => m.Name.Equals("C#")).PlatformId
            //    },
            //    new Course
            //    {
            //        Intrduction = "Đây là khóa học .NET 2",
            //        EntryTestXml = "Chưa có",
            //        Name = ".NET2",
            //        TimeLimit = 45,
            //        TuitionFee = 12000,
            //        PassMark = 50,
            //        ImageLink = "~/Content/img/course-img/c2.png",
            //        PlatformId = context.Platforms.SingleOrDefault(m => m.Name.Equals("C#")).PlatformId
            //    },
            //    new Course
            //    {
            //        Intrduction = "Đây là khóa học .NET 3",
            //        EntryTestXml = "Chưa có",
            //        Name = ".NET3",
            //        TimeLimit = 45,
            //        TuitionFee = 12000,
            //        PassMark = 50,
            //        ImageLink = "~/Content/img/course-img/c2.png",
            //        PlatformId = context.Platforms.SingleOrDefault(m => m.Name.Equals("C#")).PlatformId
            //    },
            //    new Course
            //    {
            //        Intrduction = "Đây là khóa học .NET 4",
            //        EntryTestXml = "Chưa có",
            //        Name = ".NET4",
            //        TimeLimit = 45,
            //        TuitionFee = 12000,
            //        PassMark = 50,
            //        ImageLink = "~/Content/img/course-img/c2.png",
            //        PlatformId = context.Platforms.SingleOrDefault(m => m.Name.Equals("C#")).PlatformId
            //    },
            //    new Course
            //    {
            //        Intrduction = "Đây là khóa học PHP 1",
            //        EntryTestXml = "Chưa có",
            //        Name = "PHP1",
            //        TimeLimit = 45,
            //        TuitionFee = 12000,
            //        PassMark = 50,
            //        ImageLink = "~/Content/img/portfolio/treehouse.png",
            //        PlatformId = context.Platforms.SingleOrDefault(m => m.Name.Equals("PHP")).PlatformId
            //    }
            //    );
            //context.Lessions.AddOrUpdate(
            //    m => m.LesionId,
            //    new Lession
            //    {
            //        Name = "khái niệm java",
            //        Course = context.Courses.Find(2),
            //        Description = "Rất dễ",
            //        YoutubeLink = "EE7XkaFFtGE"
            //    }
            //    );
            context.Comments.AddOrUpdate(
                m => m.CommentId,
                new Comment
                {
                    Content = "Comment 1",
                    UserId = "0425b394-541a-4d1b-91d4-10eb8f6bb3ba",
                    LessionId = 7                  
                });
        }
    }
}
