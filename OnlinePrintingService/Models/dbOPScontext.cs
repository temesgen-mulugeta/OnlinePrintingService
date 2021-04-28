using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OnlinePrintingService.Identity;

namespace OnlinePrintingService.Models
{
    public class dbOPScontext: AppDbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }



    }
}