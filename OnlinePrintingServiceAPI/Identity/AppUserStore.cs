using Microsoft.AspNet.Identity.EntityFramework;


namespace OnlinePrintingServiceApi.Identity
{
    public class AppUserStore : UserStore<AppUser>
    {
        public AppUserStore(AuthDbContext dbContext) : base(dbContext)
        {

        }
    }
}