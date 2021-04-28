using OnlinePrintingService.Helper;
using OnlinePrintingService.Identity;
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

            using (var context = new dbOPScontext())
            {
                List<string> productNames = context.Product.ToList().ConvertAll(p => p.ProductName);
                return View(productNames);
            }
        }

        public ActionResult getProductSizes(string productName)
        {

            using (var context = new dbOPScontext())
            {
                List<string> productSizes = (List<string>)context.Product.Where(p => p.ProductName.Equals(productName)).ToList().ConvertAll(p => p.ProductSize);
                return View(productSizes);
            }
        }



        [HttpPost]
        public ActionResult createOrder(OrdersViewModel orderViewModel)
        {
            using (var dbContext = new dbOPScontext())
            using (var appDbContext = new AppDbContext())
            using (var userStore = new AppUserStore(appDbContext))
            using (var userManager = new AppUserManager(userStore))
            {
                var order = new Order
                {
                    ProductID = dbContext.Product.Where(p => p.ProductName.Equals(orderViewModel.ProductName) && p.ProductSize.Equals(orderViewModel.ProductSize)).ToList().First().ProductID,
                    OrderQuantity = orderViewModel.Quantity,
                    OrderImage = orderViewModel.OrderImage,
                    UserID = userStore.FindByIdAsync(Cookie.GetCookieData(Request).userId).Result.Id
                };

                dbContext.Order.Add(order);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }

}