using Microsoft.AspNet.Identity.EntityFramework;


namespace OnlinePrintingServiceApi.Identity
{
    public class AppUserStore : UserStore<AppUser>
    {
        public AppUserStore(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}