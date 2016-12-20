using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class EmailTemplate
    {
        [Key]
        public long EmailTemplateId { get; set; }

        [DisplayName("Tiêu đề")]
        [Required]
        public string Title { get; set; }

        [DisplayName("Mô tả")]
        [Required]
        public string Description { get; set; }

        [DisplayName("Nội dung")]
        [Required]
        [AllowHtml]
        public string HtmlContent { get; set; }

    }
}