using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlinePrintingServiceAPI.Models
{
    public class Order
    {
        [Key]
        public long OrderID { get; set; }
        public long ProductID { get; set; }
        public long OrderQuantity { get; set; }
        public byte[] OrderImage { get; set; }
        public string UserID { get; set; }

    }
}