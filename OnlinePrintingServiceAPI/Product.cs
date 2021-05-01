using System.ComponentModel.DataAnnotations;

namespace OnlinePrintingServiceAPI.Models
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}