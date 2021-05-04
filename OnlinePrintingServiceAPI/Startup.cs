
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(OnlinePrintingService.Startup))]
namespace OnlinePrintingService
{
    public class Startup
    {
       

        public void CreateRolesAndUsers()
        {
            
        }
    }
}

