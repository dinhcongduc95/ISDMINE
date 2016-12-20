using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Test
    {

        public Test()
        {
            TestResults = new HashSet<TestResult>();
        }
        [Key]
        public long TestId { get; set; }

        [DisplayName("Tên")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Nội dung")]
        [Required]
        [AllowHtml]
        public string XmlContent { get; set; }

        [DisplayName("Thời gian")]
        [Required]
        public long TimeLimit { get; set; }

        [DisplayName("Điểm đạt")]
        [Required]
        public long PassMark { get; set; }

        [DisplayName("Bài giảng")]
        public Lession Lession { get; set; }

        public ICollection<TestResult> TestResults { get; set; }
    }
}