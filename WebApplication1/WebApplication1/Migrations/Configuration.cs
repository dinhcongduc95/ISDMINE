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
            context.Platforms.AddOrUpdate(
                m => m.Name,
                new []
                {
                    new Platform
                    {
                         Name = "Java",
                        Description = "Nền tảng Java có từ rất lâu"
                    },
                    new Platform
                    {
                         Name = "Php",
                        Description = "Nền tảng Php có từ rất lâu"
                    },
                    new Platform
                    {
                         Name = ".Net",
                        Description = "Nền tảng .Net có từ rất lâu"
                    },
                    new Platform
                    {
                         Name = "NodeJs",
                        Description = "Nền tảng NodeJs có từ rất lâu"
                    },
                    new Platform
                    {
                         Name = "Html/Css",
                        Description = "Nền tảng Html/Css có từ rất lâu"
                    },
                    new Platform
                    {
                         Name = "JavaScript",
                        Description = "Nền tảng JavaScript có từ rất lâu"
                    },
                }
            );
           
        }
    }
}
