using System;
using System.Collections.Generic;
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

        public string Title { get; set; }

        public string Description { get; set; }

        [AllowHtml]
        public string HtmlContent { get; set; }

    }
}