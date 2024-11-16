using Microsoft.AspNetCore.Mvc;

namespace WebBanGiay.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
