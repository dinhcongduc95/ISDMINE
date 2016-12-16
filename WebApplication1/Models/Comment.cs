using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication1.Models
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public long LessionId { get; set; }

        [MaxLength(200)]
        public string Content { get; set; }

    }
}