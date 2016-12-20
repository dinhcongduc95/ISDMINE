using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Areas.Admin.Models.ViewModels
{
    public class CourseViewModel
    {        
        public string Name { get; set; }
       
        public string Introduction { get; set; }
    }
}