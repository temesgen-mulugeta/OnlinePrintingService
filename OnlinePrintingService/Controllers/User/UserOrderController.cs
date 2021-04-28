using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers.User
{
    public class UserOrderController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult createOrder(OrdersViewModel orderViewModel)
        {
            using (var context = new dbOPScontext())

            {
                var order = new Order
                {
                    ProductID = context.Product.Where(p => p.ProductName.Equals(orderViewModel.ProductName) && p.ProductSize.Equals(orderViewModel.ProductSize)).ToList().First().ProductID,
                    OrderQuantity = orderViewModel.Quantity,    
                    OrderImage = orderViewModel.OrderImage,
                };

                context.Order.Add(order);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }

}