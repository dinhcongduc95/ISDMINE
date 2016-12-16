using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Course
    {
        [Key]
        public long CourseId { get; set; }

        public string Name { get; set; }       

        public string Intrduction { get; set; }

        public double TuitionFee { get; set; }

        [AllowHtml]
        public string EntryTestXml { get; set; }

        public long TimeLimit { get; set; }

        public long PassMark { get; set; }

        [ForeignKey("Platform")]
        public long PlatformId { get; set; }

        public string ImageLink { get; set; }
        public Platform Platform { get; set; }
    }
}