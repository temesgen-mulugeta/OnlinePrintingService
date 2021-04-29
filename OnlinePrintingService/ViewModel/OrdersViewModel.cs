using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace OnlinePrintingService.ViewModel
{
    public enum ProductName
    {

    }
    public class OrdersViewModel
    {

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Product Name can't be blank")]
        public String selectedProductName { get; set; }
        public IEnumerable<SelectListItem> ProductName {get; set;}


        [Display(Name = " Quantity")]

        [Required(ErrorMessage = "Product Price can't be blank")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please add an image")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public byte[] OrderImage { get; set; }




    }
}