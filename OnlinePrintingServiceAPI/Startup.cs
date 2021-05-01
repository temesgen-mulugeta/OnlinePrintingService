
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using OnlinePrintingServiceApi.Identity;

[assembly: OwinStartup(typeof(OnlinePrintingService.Startup))]
namespace OnlinePrintingService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/User/Login") });
            CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            using (var appDbContext = new AuthDbContext())
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(appDbContext)))
            using (var appUserStore = new AppUserStore(appDbContext))
            using (var userManager = new AppUserManager(appUserStore))
            {

                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);
                }

                if (userManager.FindByName("admin") == null)
                {
                    var user = new AppUser();
                    user.UserName = "admin";
                    user.Email = "admin@gmail.com";
                    string userPassword = "admin123";
                    var chkUser = userManager.Create(user, userPassword);
                    if (chkUser.Succeeded)
                    {
                        userManager.AddToRole(user.Id, "Admin");

                    }
                }

                if (!roleManager.RoleExists("User"))
                {
                    var role = new IdentityRole();
                    role.Name = "User";
                    roleManager.Create(role);
                }
            }
        }
    }
}

