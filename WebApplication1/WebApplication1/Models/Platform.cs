using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Platform
    {
        public Platform()
        {
            Courses = new HashSet<Course>();
        }
        [Key]
        public long PlatformId { get; set; }

        public string Name { get; set; }
        
        [AllowHtml]
        public string Description { get; set; }

        public ICollection<Course> Courses { get; set; }

    }
}