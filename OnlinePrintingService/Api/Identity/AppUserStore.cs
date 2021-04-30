using Microsoft.AspNet.Identity.EntityFramework;


namespace OnlinePrintingService.Identity
{
    public class AppUserStore : UserStore<AppUser>
    {
        public AppUserStore(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}