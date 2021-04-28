using OnlinePrintingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlinePrintingService.ViewModel;

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
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "BC", Value = "1" });
                items.Add(new SelectListItem { Text = "CC", Value = "2" });
                items.Add(new SelectListItem { Text = "DC", Value = "3" });
                ViewBag.ProductName = items;
                return View(orders);
            }
        }

        
    }
}