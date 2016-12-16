using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Rating
    {

        [Key]
        public long RatingId { get; set; }

        public long LessionId { get; set; }

        public string UserId { get; set; }
        public string Content { get; set; }

        public long Point { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("LessionId")]
        public Lession Lession { get; set; }
    }
}