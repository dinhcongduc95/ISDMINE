using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TestResult
    {
        
        [Key]
        public long TestResultId { get; set; }       

        public long TestId { get; set; }

        public string UserId { get; set; }

        public long Mark { get; set; }

        public Boolean Passed { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("TestId")]
        public Test Test { get; set; }

    }
}