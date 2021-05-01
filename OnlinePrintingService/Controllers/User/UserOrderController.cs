﻿
using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace OnlinePrintingService.Controllers.User
{
    public class UserOrderController : Controller
    {

        [HttpPost]
        public ActionResult createOrder()
        {

            var model = new OrdersViewModel();
            var productList = ProductController.getAllProducts();

            SelectList list = new SelectList(productList, "ProductID", "ProductName");
            ViewBag.drplist = list;

            var productNameListItems = productList.Select(p => new SelectListItem
            {
                Text = p.ProductName,
                Value = p.ProductName,
            });

            model.ProductName = productNameListItems;
            return View();

        }

  

        [HttpPost]
        public ActionResult createOrder(OrdersViewModel orderViewModel, HttpPostedFileBase image)
        {
            byte[] img = null;
                if (image != null)
                {
                    img = new byte[image.ContentLength];
                    image.InputStream.Read(img, 0, image.ContentLength);
                }
                var order = new Order()
                {
                    ProductID = long.Parse(orderViewModel.selectedProductName),
                    OrderQuantity = orderViewModel.Quantity,
                    OrderImage = img,
                    UserID = "silv"
                };

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44398/api/");
                var postTask = client.PostAsJsonAsync<Order>("Orders", order);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("OrderComplete", "UserOrder");
                }
            }

                ModelState.AddModelError(string.Empty, "Sorry, something went wrong.");
                return View();    
        }

        public ActionResult OrderComplete()
        {
            return View();
        }


    }

}


