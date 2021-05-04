using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Web;
using OnlinePrintingService.ViewModel;
using System.Diagnostics;
using System.Linq;
using OnlinePrintingService.Helper;

using System.Net.Http;
using System;

using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

namespace OnlinePrintingService.Controllers
{
 
    public class AppUserController : Controller
    {
        public class User
        {
          
            public string Email { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string PhoneNumber { get; set; }

            public string Password { get; set; }

            public string ConfirmPassword { get; set; }
        }
        // GET: AppUser
        public ActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        public  ActionResult SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {

                {
                    var passwordHash = Crypto.HashPassword(signUpViewModel.Password);
                    var user = new User()

                    {
                        Email = signUpViewModel.Email,
                        FirstName =signUpViewModel.FirstName,
                        LastName=signUpViewModel.LastName,  
                        PhoneNumber=signUpViewModel.PhoneNumber,
                        Password = signUpViewModel.Password,
                        ConfirmPassword = signUpViewModel.ConfirmPassword

                    }; 
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:44358/api/Account/Register");

                    var intr = client.PostAsJsonAsync<User>("Register", user);
                    intr.Wait();
                    var response = intr.Result;
                    
                    Debug.Print(response.ReasonPhrase);


                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("SignUp Error", "Invalid data");
                return View();
            }
        }



        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {

            var paramt = new List<KeyValuePair<string, string>>();
            var url = "https://localhost:44358/token";
            paramt.Add(new KeyValuePair<string, string>("grant_type", "password"));
            paramt.Add(new KeyValuePair<string, string>("username", loginViewModel.UserName));
            paramt.Add(new KeyValuePair<string, string>("password", loginViewModel.Password));
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var x = new FormUrlEncodedContent(paramt);
                HttpResponseMessage response = client.PostAsync(url,x ).Result;
                var tokne = response.Content.ReadAsStringAsync().Result;
            }
            return View();
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
