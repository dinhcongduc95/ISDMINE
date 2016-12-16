using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Enrolment
    {
        public long EnrolmentId { get; set; }

        public string UserId { get; set; }

        public long CourseId { get; set; }
        public string CreateDate { get; set; }

        public string EndDate { get; set; }

        public string IsValid { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
   
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}