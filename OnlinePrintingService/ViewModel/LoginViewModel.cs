using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlinePrintingService.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email can't be blank")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; }
    }
}