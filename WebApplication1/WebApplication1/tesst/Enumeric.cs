using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTBT.Web.Common
{
    public class Enumeric
    {
        public enum ErrorsLog
        {
            ERR_LESSIONS_RATING = 10001,
            ERR_VALIDATE_PARAM = 10002,
            ERR_UNKNOW = 10003,
            SUS_PROCESS_OK = 200,
            COMPL_RATING = 10004,
            ERR_FOUD_NOT_LESSION = 10005,
            COURSE_COMPLETED = 10006,
            LESSSION_END_COURSE = 10007,
            ERR_PROCESSING = 10000
        }

        public enum PcComplete
        {
            COMPLETED = 100,
            NONE = 0,
            NOT_COMPLETE = 50
        }
    }
}