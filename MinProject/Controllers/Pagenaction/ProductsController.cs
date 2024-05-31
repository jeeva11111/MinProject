using Microsoft.AspNetCore.Mvc;

namespace MinProject.Controllers.Pagenaction
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
