using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace OnlinePrintingService.Models
{
    public class dbOPScontext:DbContext
    {
        public dbOPScontext() : base("OnlinePrintingService") { }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }



    }
}