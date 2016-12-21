using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MTBT.Web.Models
{
    public class EntryTestResult
    {

        [Key]
        public long Id { get; set; }

        public long UserId { get; set; }

        public long CoureId { get; set; }
        
        public long Mark { get; set; }

        public bool IsPass { get; set; }

    }
}