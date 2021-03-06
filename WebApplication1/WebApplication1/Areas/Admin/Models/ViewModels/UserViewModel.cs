﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Areas.Admin.Models.ViewModels
{
    public class UserViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

        public bool IsStudent { get; set; }

        public bool IsTeacher { get; set; }

        public bool IsAdmin { get; set; }
    }
}