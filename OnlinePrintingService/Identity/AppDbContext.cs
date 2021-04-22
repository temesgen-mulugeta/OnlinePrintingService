using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlinePrintingService.Identity
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("DefualtConnection") { }

        public System.Data.Entity.DbSet<OnlinePrintingService.Identity.AppUser> AppUsers { get; set; }
    }
}