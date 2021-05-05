using OnlinePrintingService.REST;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class AdminOrderController : Controller
    {
        public class OrderResult
        {
            public long orderId { set; get; }
            public string customerName { set; get; }

            public string phoneNumber { set; get; }
            public string productName { set; get; }
            public long quantity { set; get; }
            public byte[] image { set; get; }
        }


        public ActionResult Order()
        {
            var orderList = OrderREST.GetAllOrders();
            var productList = ProductREST.GetAllProducts();
            var userList = UserREST.GetAllUsers();

           var OrderResultList = (
                                    from order in orderList
                                    join product in productList on order.ProductID equals product.ProductID
                                    join user in userList on order.UserID equals user.Email
                                    where order.ProductID == product.ProductID 
                                    where order.UserID == user.Email
                                                    select new OrderResult
                                                    {
                                                        orderId = order.OrderID,
                                                        quantity = order.OrderQuantity,
                                                        image = order.OrderImage,
                                                        productName = product.ProductName,
                                                        customerName = user.FirstName + user.LastName,
                                                        phoneNumber=user.PhoneNumber
                                                    }).ToList();
            ViewBag.orders = OrderResultList;
            return View();

        }




        /*



        //  var context = new dbOPScontext();
        //var userStore = new AppUserStore(context);
        List < OrderList > ordList = new List<OrderList>();
        OrderList od = null;
       // List<Order> orders = context.Order.ToList();
        foreach (var ordr in orders)
        {
           od = new OrderList();
            od.orderId = ordr.OrderID;
           // od.productName  = context.Product.Where(p => p.ProductID.Equals(ordr.ProductID)).ToList().First().ProductName;
            int x = ordr.UserID.IndexOf("&role");
            string id = ordr.UserID.Substring(0, x);
           // od.customerName = context.Users.Where(c => c.Id.Equals(id)).ToList().First().UserName;
            od.quantity = ordr.OrderQuantity;
            od.image = ordr.OrderImage;
            ordList.Add(od);
        }

        /*


        using (var context1 = new dbOPScontext())
        {

            var getprdList = context.Product.ToList();
            SelectList list = new SelectList(getprdList, "ProductID", "ProductName");
            ViewBag.drplist = list;



        }



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

            ViewBag.orders = ordList;

            return View();

    }
*/
        public ActionResult DeleteOrder(long OrderID)
        {
            OrderREST.Delete(OrderID);
            return RedirectToAction("Order", "AdminOrder");          
        }
          


    }
}

