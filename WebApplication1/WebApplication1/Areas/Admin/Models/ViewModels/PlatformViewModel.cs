using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Areas.Admin.Models.ViewModels
{
    public class PlatformViewModel
    {        
        public long PlatformId { get; set; }

        public string Name { get; set; }
       
        public string Description { get; set; }
      
    }
}