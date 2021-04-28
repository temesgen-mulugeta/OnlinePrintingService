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

        public ActionResult createOrder()


        {
            var model = new OrdersViewModel();
            using (var context = new dbOPScontext())
            {
                List<string> productNames = context.Product.ToList().ConvertAll(p => p.ProductName);
              
                model.ProductName = GetSelectListItems(productNames);
               
            }
            return View();
           
        }

        public ActionResult getProductSizes(string productName)
        {
            using (var context = new dbOPScontext())
            {
                List<string> productSizes = context.Product.Where(p => p.ProductName.Equals(productName)).ToList().ConvertAll(p => p.ProductSize);
                return View("createOrder", productSizes);
            }
        }

        [HttpPost]
        public ActionResult createOrder(OrdersViewModel orderViewModel)
        {
            using (var dbContext = new dbOPScontext())
            using (var appDbContext = new AppDbContext())
            using (var userStore = new AppUserStore(appDbContext))
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
        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();
            var id = 0;
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = id++.ToString() ,
                    Text = element
                });
            }
            return selectList;
        }
    }

}