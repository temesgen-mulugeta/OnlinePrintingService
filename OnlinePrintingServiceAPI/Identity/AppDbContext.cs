
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlinePrintingServiceApi.Identity
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("DefaultConnection") { }

    }
}