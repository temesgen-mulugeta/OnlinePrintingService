using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlinePrintingService.Models
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductSize { get; set; }
        public decimal Price { get; set; }
    }
}