using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Course
    {

        public Course()
        {
            Lessions = new HashSet<Lession>();
            Enrolments = new HashSet<Enrolment>();
        }

        [Key]
        public long CourseId { get; set; }

        [DisplayName("Tên")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Mô tả")]
        [AllowHtml]
        [Required]
        public string Introduction { get; set; }

        [DisplayName("Học phí")]
        [Required]
        public double TuitionFee { get; set; }

        [DisplayName("Test")]
        [Required]
        [AllowHtml]
        public string EntryTestXml { get; set; }

        [DisplayName("Thời gian")]
        [Required]
        public long TimeLimit { get; set; }

        [DisplayName("Điểm đạt")]
        [Required]
        public long PassMark { get; set; }

        [DisplayName("Ảnh")]
        [Required]
        public string ImageLink { get; set; }

        [DisplayName("Cấp độ")]
        [Required]
        public string Level { get; set; }

        [DisplayName("Nền tảng")]           
        public Platform Platform { get; set; }

        public ICollection<Lession> Lessions { get; set; }

        public ICollection<Enrolment> Enrolments { get; set; }
    }
}