
using OnlinePrintingService.Models;
using OnlinePrintingService.ViewModel;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using OnlinePrintingService.REST;

namespace OnlinePrintingService.Controllers.User
{
    public class UserOrderController : Controller
    {
        public ActionResult createOrder()
        {
            if (!OnlinePrintingService.Helper.Cookiez.isUserLoggedIn(Request))
            {
                return RedirectToAction("Login", "AppUser");
            }
            var model = new OrdersViewModel();
            var productList = ProductREST.GetAllProducts();

            SelectList list = new SelectList(productList, "ProductID", "ProductName");
            ViewBag.drplist = list;

            var productNameListItems = productList.Select(p => new SelectListItem
            {
                Text = p.ProductID.ToString(),
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
                UserID = OnlinePrintingService.Helper.Cookiez.GetCookieData(Request).userId
            };

            var result = OrderREST.Post(order);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("OrderComplete", "UserOrder");
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


