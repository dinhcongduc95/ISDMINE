using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Comment
    {

        [Key]
        public long CommentId { get; set; }         

        [DisplayName("Nội dung")]
        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [DisplayName("Sao")]
        [Required]
        public int Star { get; set; }

        [DisplayName("Người dùng")]
        public ApplicationUser User { get; set; }

        [DisplayName("Bài học")]        
        public Lession Lession { get; set; }
    }
}