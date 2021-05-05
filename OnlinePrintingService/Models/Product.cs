using System.ComponentModel.DataAnnotations;

namespace OnlinePrintingService.Models
{
    public class Product
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}