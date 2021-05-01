
using Microsoft.AspNet.Identity;


namespace OnlinePrintingService.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {

        }

    }
}