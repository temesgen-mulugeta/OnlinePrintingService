using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlinePrintingService.Identity
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("DefualtConnection") { }

        public System.Data.Entity.DbSet<OnlinePrintingServiceAPI.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<OnlinePrintingServiceAPI.Models.Product> Products { get; set; }
    }
}