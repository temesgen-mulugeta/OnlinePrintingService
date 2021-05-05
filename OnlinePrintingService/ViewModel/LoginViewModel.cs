using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlinePrintingService.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username can't be blank")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; }


          
    }
}