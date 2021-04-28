using OnlinePrintingService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class AdminOrderController : Controller
    {
        public ActionResult Order()
        {
            using (var context = new dbOPScontext())
            {
                List<Order> orders = context.Order.ToList();
                return View(orders);
            }
        }

        public ActionResult removeOrder(long OrderID)
        {
            using (var context = new dbOPScontext())
            {
                var order = new Order
                {
                    OrderID = OrderID,
                };

                context.Order.Remove(order);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }


    }
}