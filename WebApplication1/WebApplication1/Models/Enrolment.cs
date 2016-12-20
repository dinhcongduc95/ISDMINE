using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Enrolment
    {
        [Key]
        public long EnrolmentId { get; set; }

        [DisplayName("Ngày bắt đầu")]
        [Required]
        public string CreateDate { get; set; }

        [DisplayName("Ngày kết thúc")]
        [Required]
        public string EndDate { get; set; }

        [DisplayName("Hiệu lực")]
        [Required]
        public bool IsValid { get; set; }

        [DisplayName("Khóa học")]        
        public Course Course { get; set; }

        [DisplayName("Người dùng")]
        public ApplicationUser User { get; set; }
           
    }
}