﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlinePrintingService.Models
{
    public class AppUser
    {

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}