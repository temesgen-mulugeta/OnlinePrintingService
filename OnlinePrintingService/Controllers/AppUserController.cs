using Microsoft.AspNet.Identity;
using OnlinePrintingService.Identity;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Web;
using OnlinePrintingService.ViewModel;
using System.Diagnostics;
using System.Linq;

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
    }
}