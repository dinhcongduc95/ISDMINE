using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Common
{
    public class Constant
    {

        public Constant()
        {
            
        }

        #region Nested type: Roles

        public class RoleNames
        {
            public const string ADMINISTRATOR = "Administrator";
            public const string SUBMITTER = "Submitter";
            public const string USER = "User";
            public const string REGISTER = "Register";
            public const string APPROVER = "Approver";
            public const string TEACHER = "Teacher";
            public const string ADMINISTRATOR_AND_SUBMITTER = "Administrator,Submitter";
            public const string ADMINISTRATOR_AND_TEACHER = "Administrator,Teacher";
            public const string USER_AND_SUBMITTER = "User,Submitter";
            public const string SUMMITER_AND_TEACHER = "Submitter,Teacher";
            public const string ADMINISTRATOR_AND_SUBMITTER_AND_TEACHER = "Administrator,Submitter,Teacher";
        }

        #endregion
    }
}