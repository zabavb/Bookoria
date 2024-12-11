using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
