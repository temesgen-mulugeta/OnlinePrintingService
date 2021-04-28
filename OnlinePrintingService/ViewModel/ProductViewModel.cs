using System.ComponentModel.DataAnnotations;

namespace OnlinePrintingService.ViewModel
{
    public class ProductViewModel
    {

        [Required(ErrorMessage = "Product Name can't be blank")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Size can't be blank")]
        public string ProductSize { get; set; }

        [Required(ErrorMessage = "Product Price can't be blank")]
        public decimal Price { get; set; }
     
    }
}