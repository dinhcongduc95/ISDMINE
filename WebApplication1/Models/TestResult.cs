using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TestResult
    {
        [Key]
        public long Id { get; set; }

        public long UserId { get; set; }

        public long TestId { get; set; }

        public long Mark { get; set; }

        public bool Passed { get; set; }

    }
}