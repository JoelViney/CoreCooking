using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCooking.Website.ViewModels.Accounts
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            this.Name = null;
            this.Password = null;
            this.RememberMe = true;
        }

        [Required, DisplayName("User Name")]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }

        public string Message { get; set; }

        public string ReturnUrl { get; set; }

    }
}
