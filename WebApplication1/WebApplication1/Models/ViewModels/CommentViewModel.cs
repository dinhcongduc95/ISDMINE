using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class Comment
    {

        [Key]
        public long CommentId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("Lession")]
        public long LessionId { get; set; }
        public Lession Lession { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}