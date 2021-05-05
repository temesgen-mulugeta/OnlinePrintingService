using System;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlinePrintingServiceApi.Identity;
using OnlinePrintingServiceAPI.Models;

namespace OnlinePrintingServiceAPI.Controllers
{
    [RoutePrefix("api/Account")]
    public class AuthController : ApiController
    {

        private ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        [Route("Register")]

        // POST: api/Register
        public IHttpActionResult PostRegister(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                FirstName=model.FirstName,
                LastName=model.LastName,
                PhoneNumber=model.PhoneNumber,
                Email = model.Email,
                UserName = model.Email,
                EmailConfirmed = true
            };

            var result = UserManager.Create(user, model.Password);
            return result.Succeeded ? Ok() : GetErrorResult(result);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }

    }
}