
using Microsoft.AspNet.Identity;


namespace OnlinePrintingServiceApi.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) : base(store)
        {

        }

    }
}