﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.appstore.Models
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsActive { get; set; }
        public string Token { get; set; }
        public DateTime TokenValidity { get; set; }
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
    }
}