using Microsoft.AspNet.Identity;
using OnlinePrintingService.Identity;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Web;
using OnlinePrintingService.ViewModel;
using System.Diagnostics;
using System.Linq;
using OnlinePrintingService.Helper;

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
        public ActionResult SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var appDbContext = new AppDbContext())
                using (var userStore = new AppUserStore(appDbContext))
                using (var userManager = new AppUserManager(userStore))
                {
                    var passwordHash = Crypto.HashPassword(signUpViewModel.Password);
                    var user = new AppUser()

                    {
                    UserName = signUpViewModel.UserName,
                    Email = signUpViewModel.Email,
                    PasswordHash = passwordHash,
                    PhoneNumber = signUpViewModel.PhoneNumber
                    };
                    IdentityResult result = userManager.Create(user);
                    if (result.Succeeded)
                    {


                        userManager.AddToRole(user.Id, "User");

                        var authenticationManager = HttpContext.GetOwinContext().Authentication;
                        var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                        Cookie.AddCookie(user.UserName, "Admin", Response);

                    }
                    else
                    {
                        Debug.Print(string.Join("\n", result.Errors.ToArray()));
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            
            using (var appDbContext = new AppDbContext())
            using (var userStore = new AppUserStore(appDbContext))
            using (var userManager = new AppUserManager(userStore))
            {
                var user = userManager.Find(loginViewModel.UserName, loginViewModel.Password);
                if (user != null)
                {
                    var authenticationManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                    if (userManager.IsInRole(user.Id, "Admin"))
                    {
                        Cookie.AddCookie(user.Id, "Admin", Response);
                        return RedirectToAction("Order", "AdminOrder", new { area = "Admin" });

                    }
                    else
                    {
                        Cookie.AddCookie(user.Id, "User", Response);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Authentication Error", "Invalid username and/or password");
                    return View();
                }
            }


        }

        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Cookie.RemoveCookie(HttpContext.ApplicationInstance.Response);
            return RedirectToAction("Index", "Home");
        }

     
    }
}