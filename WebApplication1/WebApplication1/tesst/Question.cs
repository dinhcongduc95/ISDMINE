using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTBT.Web.Models
{
    public class Question
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public List<string> Answers { get; set; }
        public int Result { get; set; }
        public Question()
        {
            Answers = new List<string>();
        }
    }

    public class Questions
    {
        public List<Question> questions { get; set; }

        public Questions()
        {
            questions= new List<Question>();
        }
    }

}