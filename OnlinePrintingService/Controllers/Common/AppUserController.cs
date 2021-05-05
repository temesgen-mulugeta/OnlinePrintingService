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
using OnlinePrintingService.Models;
using OnlinePrintingService.ApiInfrastructure;

namespace OnlinePrintingService.Controllers
{
    public abstract class BaseController : Controller
    {
        protected void AddResponseErrorsToModelState(ApiResponse response)
        {
            var errors = response.ErrorState.ModelState;
            if (errors == null)
            {
                return;
            }

            foreach (var error in errors)
            {
                foreach (var entry in
                    from entry in ModelState
                    let matchSuffix = string.Concat(".", entry.Key)
                    where error.Key.EndsWith(matchSuffix)
                    select entry)
                {
                    ModelState.AddModelError(entry.Key, error.Value[0]);
                }
            }
        }
    }
    public class UserData
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string userName { get; set; }
                public string issued { get; set; }
        public string expires { get; set; }
       
    }
    public class AccountController : BaseController
    {
        private readonly ILoginClient loginClient;
        private readonly ITokenContainer tokenContainer;

        /// <summary>
        /// Default parameterless constructor. 
        /// Delete this if you are using a DI container.
        /// </summary>
        public AccountController()
        {
            tokenContainer = new TokenContainer();
            var apiClient = new ApiClient(new HttpClient());
            loginClient = new LoginClient(apiClient);
        }

        /// <summary>
        /// Default constructor with dependency.
        /// Delete this if you aren't planning on using a DI container.
        /// </summary>
        /// <param name="loginClient">The login client.</param>
        /// <param name="tokenContainer">The token container.</param>
        public AccountController(ILoginClient loginClient, ITokenContainer tokenContainer)
        {
            this.loginClient = loginClient;
            this.tokenContainer = tokenContainer;
        }

        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var loginSuccess = await PerformLoginActions(model.UserName, model.Password);
            if (loginSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.Clear();
            ModelState.AddModelError("", "The username or password is incorrect");
            return View(model);
        }

        // Register methods go here, removed for brevity

        private async Task<bool> PerformLoginActions(string email, string password)
        {
            var response = await loginClient.Login(email, password);
            if (response.StatusIsSuccessful)
            {
                tokenContainer.ApiToken = response.Data;
            }
            else
            {
                AddResponseErrorsToModelState(response);
            }

            return response.StatusIsSuccessful;
        }
    }
    public class AppUserController : Controller
    {
       


        public class Userx
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
                    var user = new Userx()

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

                   // var intr = client.PostAsJsonAsync<User>("Register", user);
                    //intr.Wait();
                    //var response = intr.Result;
                    
                   // Debug.Print(response.ReasonPhrase);


                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("SignUp Error", "Invalid data");
                return View();
            }
        }


        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {

            //var x= new LoginViewModel(loginViewModel.UserName, loginViewModel.Password);

         

            if (UserInformation.response != null)
            {
                var pp = UserInformation.response.Content;
                if (UserInformation.message == "ok")
                {


                    return RedirectToAction("Index", "Home");
                }
            }

            if (UserInformation.message != null)
            {
                var pp = UserInformation.response.Content;
                if (UserInformation.message == "ok")
                {


                    return RedirectToAction("Index", "Home");
                }
            }

            return View();

          
        }
        public async Task check(string UserName, string Password)
        {
            string user = UserName;
            string pass = Password;
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));
            nvc.Add(new KeyValuePair<string, string>("username", user));
            nvc.Add(new KeyValuePair<string, string>("password", pass));
            var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44358/token") { Content = new FormUrlEncodedContent(nvc) };
            var res = await client.SendAsync(req);
            UserInformation.response = res;
            UserInformation.message = res.ReasonPhrase;

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
