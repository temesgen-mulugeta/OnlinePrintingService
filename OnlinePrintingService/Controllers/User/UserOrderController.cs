using OnlinePrintingService.Helper;
using OnlinePrintingService.Identity;
using OnlinePrintingService.ViewModel;
using OnlinePrintingServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers.User
{
    public class UserOrderController : Controller
    {

        public ActionResult createOrder()


        {
            var loggedin = OnlinePrintingService.Helper.Cookie.isUserLoggedIn(Request);
           // if (!loggedin)
            {
               // return RedirectToAction("Login", "AppUser");
            }

            var model = new OrdersViewModel();
            using (var context = new dbOPScontext())
            {

                var getprdList = context.Product.ToList();
                SelectList list = new SelectList(getprdList, "ProductID", "ProductName");
                ViewBag.drplist = list;
               // List<string> productNames = context.Product.ToList().ConvertAll(p => p.ProductName);
                //model.ProductName = GetSelectListItems(productNames);            


            }
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
                    client.BaseAddress = new Uri("api");
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