using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Test
    {
        [Key]
        public long Id { get; set; }

        public long LessionId { get; set; }

        public string Name { get; set; }

        public string XmlContent { get; set; }

        public int TimeLimit { get; set; }

        public int PassMark { get; set; }

    }
}