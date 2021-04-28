using OnlinePrintingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Order()
        {
            using (var context = new dbOPScontext())
            {
                List<Order> orders = context.Order.ToList();
                return View(orders);
            }
        }
    }
}