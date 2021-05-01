using System;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OnlinePrintingServiceApi.Identity;

namespace OnlinePrintingServiceAPI.Controllers
{
    public class AuthController : ApiController
    {
        private static AuthDbContext db = new AuthDbContext();
        private static AppUserStore userStore = new AppUserStore(db);
        private static AppUserManager userManager = new AppUserManager(userStore);


        // POST: api/Auth

        [ResponseType(typeof(void))]
        public IHttpActionResult Register(AppUser appUser)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            if (userManager.FindById(appUser.Id) == null)
            {

                IdentityResult result = userManager.Create(appUser);

                if (result.Succeeded)
                {
                    userManager.AddToRole(appUser.Id, "User");
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(appUser, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);

                    // ADD COOKIE HERE

                    return Ok();

                }
            }

            return CreatedAtRoute("DefaultApi", new { id = appUser.Id }, appUser);

        }

        // GET: api/Auth

        [ResponseType(typeof(void))]
        public IHttpActionResult Login(string credentials)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            var encoding = Encoding.GetEncoding("iso-8859-1");
            credentials = encoding.GetString(Convert.FromBase64String(credentials));
            int separator = credentials.IndexOf(':');
            string name = credentials.Substring(0, separator);
            string password = credentials.Substring(separator + 1);


            var user = userManager.Find(name, password);
            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);

                if (userIdentity.IsAuthenticated)
                {
                    return Ok();
                }




                if (userManager.IsInRole(user.Id, "Admin")) 
                {

                    // ADD COOKIE HERE

                }
                else
                {
                    // ADD COOKIE HERE

                }
            }
            else
            {
                ModelState.AddModelError("Authentication Error", "Invalid username and/or password");
            }

            return BadRequest();

        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                userStore.Dispose();
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}