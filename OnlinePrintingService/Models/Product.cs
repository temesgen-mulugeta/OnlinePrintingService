using System.ComponentModel.DataAnnotations;

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