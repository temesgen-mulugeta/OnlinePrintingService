
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlinePrintingServiceApi.Identity
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext() : base("DefaultConnection") { }
    }
}