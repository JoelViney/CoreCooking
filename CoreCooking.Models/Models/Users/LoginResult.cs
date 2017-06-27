using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCooking.Models.Users
{
    public class LoginResult
    {
        public User User { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
