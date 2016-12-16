using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Course
    {
        [Key]
        public long Id { get; set; }

        public long PlatformId { get; set; }

        public string Name { get; set; }

        public string Introduction { get; set; }

        public long TuitionFee { get; set; }

        public string EntryTestXml { get; set; }

        public int TimeLimit { get; set; }

        public int PassMark { get; set; }
        
    }
}