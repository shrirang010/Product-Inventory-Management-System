using Microsoft.AspNetCore.Mvc;

namespace Product_Inventory_Management_System.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
