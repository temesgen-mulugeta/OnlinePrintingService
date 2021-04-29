using OnlinePrintingService.Helper;
using OnlinePrintingService.Identity;
using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers.User
{
    public class UserOrderController : Controller
    {

        public ActionResult createOrder()


        {
            var loggedin = OnlinePrintingService.Helper.Cookie.isUserLoggedIn(Request);
            if (!loggedin)
            {
                return RedirectToAction("Login", "AppUser");
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
        public ActionResult createOrder(OrdersViewModel orderViewModel, HttpPostedFileBase image1)
        {
            using (var dbContext = new dbOPScontext())
            using (var appDbContext = new AppDbContext())
            using (var userStore = new AppUserStore(appDbContext))
            {

                byte[] img=null;
                if(image1 != null)
                {
                    img = new byte[image1.ContentLength];
                    image1.InputStream.Read(img, 0, image1.ContentLength);
                }
                var order = new Order();

                order.ProductID = long.Parse(orderViewModel.selectedProductName);
                order.OrderQuantity = orderViewModel.Quantity;
                order.OrderImage = img;
                order.UserID = Cookie.GetCookieData(Request).userId;
                

                dbContext.Order.Add(order);
                dbContext.SaveChanges();
                return RedirectToAction("OrderComplete", "UserOrder");
            }
        }

        public ActionResult OrderComplete()
        {
            return View();
        }


    }

}