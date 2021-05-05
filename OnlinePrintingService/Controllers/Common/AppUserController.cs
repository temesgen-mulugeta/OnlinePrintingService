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
using System.Threading.Tasks;

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
        public async Task< ActionResult> Login(LoginViewModel loginViewModel)
        {

            var paramt = new List<KeyValuePair<string, string>>();
           
            paramt.Add(new KeyValuePair<string, string>("grant_type", "password"));
            paramt.Add(new KeyValuePair<string, string>("username", loginViewModel.UserName));
            paramt.Add(new KeyValuePair<string, string>("password", loginViewModel.Password));
            
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44358/token") { Content = new FormUrlEncodedContent(paramt) };
            var res = await client.SendAsync(req);
            string responseBody = await res.Content.ReadAsStringAsync();

            Authn userAuth = JsonConvert.DeserializeObject<Authn>(responseBody);
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Cookie.AddCookie(userAuth.userName, "User", Response);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("SignUp Error", "Invalid data");
            return View();
        }

            public ActionResult Logout()
        {
            
            Cookie.RemoveCookie(HttpContext.ApplicationInstance.Response);
            return RedirectToAction("Index", "Home");
        }

        
    }

    public class Authn
    {
        public string access_token { set; get; }
        public string token_type { set; get; }
        public string expires_in { set; get; }
        public string userName { set; get; }
    }
}
