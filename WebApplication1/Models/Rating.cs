using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Rating
    {
        [Key]
        public long Id { get; set; }

        public long LessionId { get; set; }

        public long UserId { get; set; }

        public string Content { get; set; }

        public int Point { get; set; }

    }
}