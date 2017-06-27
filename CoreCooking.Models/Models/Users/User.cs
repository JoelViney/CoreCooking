using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCooking.Models.Users
{
    public class User : ModelBase
    {
        public User()
        {

        }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public override Guid Guid { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
