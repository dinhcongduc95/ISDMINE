using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.ViewModels
{
    public class Platform
    {

        [Key]
        public long PlatformId { get; set; }

        public string Name { get; set; }
        
        [AllowHtml]
        public string Description { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

    }
}