using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlinePrintingService.Identity
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("DefualtConnection") { }
    }
}