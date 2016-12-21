using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Tutorial
    {
        [Key]
        public int TutorialId { get; set; }

        public string Name { get; set; }

        [AllowHtml]
        public string Content { get; set; }

    }
}