using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Web;
using OnlinePrintingService.ViewModel;
using System.Diagnostics;
using System.Linq;
using OnlinePrintingService.Helper;
using OnlinePrintingService.Identity;
using System.Net.Http;
using System;

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

                {
                    var passwordHash = Crypto.HashPassword(signUpViewModel.Password);
                    var user = new AppUser()

                    {
                        UserName = signUpViewModel.UserName,
                        Email = signUpViewModel.Email,
                        PasswordHash = passwordHash,
                        PhoneNumber = signUpViewModel.PhoneNumber
                    };

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44398/api/");
                        var postTask = client.PostAsJsonAsync<AppUser>("Auth", user);
                        postTask.Wait();
                        var result = postTask.Result;

                        if (result.IsSuccessStatusCode)
                        {

                           // Cookie.AddCookie(user.UserName, "Admin", Response);

                        }
                        else
                        {
                            //Debug.Print(string.Join("\n", result..ToArray()));
                        }
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

        public ActionResult Login() => View();


       [HttpGet]
        public ActionResult Login(LoginViewModel loginViewModel)
        {


            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44398/api/");
                    var getTask = client.GetAsync("Auth/" + $"{loginViewModel.UserName}:{loginViewModel.Password}");
                    getTask.Wait();
                    var result = getTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        Debug.Print("yeeyyyyyy");

                        // Cookie.AddCookie(user.UserName, "Admin", Response);
                        return View();


                    }
                    else
                    {
                        Debug.Print(result.ReasonPhrase);

                        ModelState.AddModelError("Authentication Error", "Invalid username and/or password");
                        return View();
                    }
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
