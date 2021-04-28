using System.Web.Mvc;

namespace OnlinePrintingService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Pricing()
        {
            return View();
        }
    }
}