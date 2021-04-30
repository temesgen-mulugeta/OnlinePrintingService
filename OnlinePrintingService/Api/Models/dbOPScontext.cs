using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OnlinePrintingService.Identity;

namespace OnlinePrintingServiceAPI.Models
{
    public class dbOPScontext: DbContext
    {

        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }



    }
}