using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet("Admin/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Admin/Category")]
        public IActionResult Category()
        {
            return View();
        }

        [HttpGet("Admin/Product")]
        public IActionResult Product()
        {
            return View();
        }

    }
}
