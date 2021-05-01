
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlinePrintingServiceApi.Identity
{
    public class AuthDbContext : IdentityDbContext<AppUser>
    {
        public AuthDbContext() : base("DefaultConnection") { }
    }
}