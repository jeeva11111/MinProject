using Microsoft.AspNetCore.Mvc;

namespace MinProject.Controllers.Home
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
