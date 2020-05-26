﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Domain.Models
{
    public class UserAccount : DomainBase
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
