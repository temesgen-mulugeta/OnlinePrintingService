using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace OnlinePrintingService.ViewModel
{
    public enum ProductName
    {

    }
    public class OrdersViewModel
    {
        [Required(ErrorMessage = "Product Name can't be blank")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Size can't be blank")]
        public string ProductSize { get; set; }

        [Required(ErrorMessage = "Product Price can't be blank")]
        public int Quantity { get; set; }




    }
}