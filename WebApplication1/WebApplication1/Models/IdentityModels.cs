﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string HomeTown { get; set; }

        public System.DateTime? BirthDate { get; set; }
        
        public ICollection<Enrolment> Enrolments { get; set; }

        public ICollection<Email> Emails { get; set; }

        public ICollection<TestResult> TestResults { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lession> Lessions { get; set; }
        public virtual DbSet<Enrolment> Enrolments { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }        
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<Email> Emails { get; set; }

        public virtual DbSet<Tutorial> Tutorials { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
       
        public static ApplicationDbContext Create()
        {
            
            return new ApplicationDbContext();
        }
    }
}