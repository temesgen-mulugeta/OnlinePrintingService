using System.Data.Entity;

namespace OnlinePrintingServiceAPI.Models
{
    public class dbOPScontext : DbContext
    {

        public dbOPScontext() : base("DefaultConnection"){}
 
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }



    }
}