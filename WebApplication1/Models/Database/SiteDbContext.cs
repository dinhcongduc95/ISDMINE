using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Database
{
    public class SiteDbContext : DbContext
    {
        public SiteDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Lession> Lessions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
    }
}