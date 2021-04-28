using OnlinePrintingService.Identity;
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

            var context = new dbOPScontext();
            var userStore = new AppUserStore(context);
            
                var orders = (from order in context.Order
                              join product in context.Product on order.ProductID equals product.ProductID
                              join user in context.Users on order.UserID equals user.Id
                              where order.UserID == user.Id && order.ProductID == product.ProductID
                              select new
                              {
                                  OrderId = order.OrderID,
                                  CustomerName = user.UserName,
                                  ProductName = product.ProductName,
                                  Image = order.OrderImage,
                              });

                ViewBag.orders = orders;

                return View();
            
        }

        public ActionResult removeOrder(long OrderID)
        {
            using (var context = new dbOPScontext())
            {
                var order = new Order
                {
                    OrderID = OrderID,
                };
                context.Order.Attach(order);
                context.Order.Remove(order);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }


    }
}