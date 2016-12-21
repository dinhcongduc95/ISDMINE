using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Bài kiểm tra")]
        public Test Test { get; set; }

        [DisplayName("Người dùng")]        
        public ApplicationUser User { get; set; }

        [DisplayName("Điểm số")]
        [Required]
        public long Mark { get; set; }

        [DisplayName("Đã qua")]
        [Required]
        public bool Passed { get; set; }
        
    }
}