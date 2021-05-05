using Microsoft.AspNet.Identity.EntityFramework;


namespace OnlinePrintingServiceApi.Identity
{
    public class AppUserStore : UserStore<ApplicationUser>
    {
        public AppUserStore(AuthDbContext dbContext) : base(dbContext)
        {

        }
    }
}