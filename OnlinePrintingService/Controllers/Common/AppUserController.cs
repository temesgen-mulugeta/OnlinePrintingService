using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Web;
using OnlinePrintingService.ViewModel;
using System.Diagnostics;
using System.Linq;
using OnlinePrintingService.Helper;
using OnlinePrintingService.Models;
 using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace OnlinePrintingService.Controllers
{
    public class AppUserController : Controller
    {
        public class UserAuth
        {
            public string access_token { get; set; }
            public string token_type { get; set; }

            public string expires_in { get; set; }

            public string userName { get; set; }

            public string issued { get; set; }
            public string expires { get; set; }

        }
        
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
                        Email = signUpViewModel.Email,
                        FirstName = signUpViewModel.FirstName,
                        LastName = signUpViewModel.LastName,
                        PhoneNumber = signUpViewModel.PhoneNumber,
                        Password = signUpViewModel.Password,
                        ConfirmPassword = signUpViewModel.ConfirmPassword

                    };
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:44398/api/Account/Register");

                    var intr = client.PostAsJsonAsync<AppUser>("Register", user);
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


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));
            nvc.Add(new KeyValuePair<string, string>("username", loginViewModel.UserName));
            nvc.Add(new KeyValuePair<string, string>("password", loginViewModel.Password));
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44398/api/token") { Content = new FormUrlEncodedContent(nvc) };
            var res = await client.SendAsync(req);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                
                string responseBody = await res.Content.ReadAsStringAsync();
                UserAuth usr = JsonConvert.DeserializeObject<UserAuth>(responseBody);

               
                if (usr.userName == "admin") {
                    Cookiez.AddCookie(usr.userName, "Admin", Response);
                    return RedirectToAction("Order", "AdminOrder");
                }
                Cookiez.AddCookie(usr.userName, "User", Response);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("SignUp Error", "Invalid data");
            return View();
        }

       
        public ActionResult Logout()
        {
            
            Cookiez.RemoveCookie(HttpContext.ApplicationInstance.Response);
            return RedirectToAction("Index", "Home");
        }

        
    }
}
