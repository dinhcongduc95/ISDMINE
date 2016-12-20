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
    public class Lession
    {

        public Lession()
        {
            Comments = new HashSet<Comment>();            
            Tests = new HashSet<Test>();
        }

        [Key]
        public long LesionId { get; set; }
       
        [DisplayName("Tên")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Mô tả")]
        [Required]
        [AllowHtml]
        public string Description { get; set; }

        [DisplayName("Video")]
        [Required]
        public string YoutubeLink { get; set; }

        [DisplayName("Ảnh")]
        [Required]
        public string ImageLink { get; set; }

        [DisplayName("Khóa học")]   
        public Course Course { get; set; }

        public ICollection<Comment> Comments { get; set; }        

        public ICollection<Test> Tests { get; set; }
    }
}