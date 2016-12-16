using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.ViewModels
{
    public class Lession
    {

        [Key]
        public long LesionId { get; set; }

        [ForeignKey("Course")]
        public long CourseId { get; set; }
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public string YoutubeLink { get; set; }

        
        public Course Course { get; set; } 
  
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; } 

        public virtual ICollection<Test> Tests { get; set; }
    }
}