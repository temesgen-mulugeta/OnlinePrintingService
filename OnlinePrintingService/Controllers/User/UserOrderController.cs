using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System.Collections.Generic;
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
                   // ProductName = productViewModel.ProductName,
                   // ProductSize = productViewModel.ProductSize,
                   // Price = productViewModel.Price
                };

                context.Order.Add(order);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }

}