using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Test
    {

        [Key]
        public long TestId { get; set; }

        
        public long LessionId { get; set; }

        public string Name { get; set; }

        [AllowHtml]
        public string XmlContent { get; set; }

        public long TimeLimit { get; set; }

        public long PassMark { get; set; }

        [ForeignKey("LessionId")]
        public Lession Lession { get; set; }

    }
}