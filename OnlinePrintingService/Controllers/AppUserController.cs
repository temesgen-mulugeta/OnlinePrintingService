﻿using Microsoft.AspNet.Identity;
using OnlinePrintingService.Identity;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Web;



namespace OnlinePrintingService.Controllers
{
    public class AppUserController : Controller
    {
        // GET: AppUser
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(int x)
        {
            if (ModelState.IsValid)
            {
                using (var appDbContext = new AppDbContext())
                using (var userStore = new AppUserStore(appDbContext))
                using (var userManager = new AppUserManager(userStore))
                {
                   // var passwordHash = Crypto.HashPassword(rvm.Password);
                    var user = new AppUser()

                    {
                    //    Email = rvm.Email,
                     //   PasswordHash = passwordHash,
                     //   PhoneNumber = rvm.Mobile
                    };
                    IdentityResult result = userManager.Create(user);

                    if (result.Succeeded)
                    {
                        userManager.AddToRole(user.Id, "user");

                        var authenticationManager = HttpContext.GetOwinContext().Authentication;
                        var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("SignUp Error", "Invalid data");
                return View();       
            }
        }
    }
}