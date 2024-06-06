using Microsoft.AspNetCore.Mvc;
using MinProject.Data;
using MinProject.Views.GenerateDynamicdataViews;

namespace MinProject.Controllers.GenerateDynamicdata
{
    public class GenerateController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GenerateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var listOfProducts = _context.Products.ToList();
            var country = _context.Countries.ToList();
            var model = new IndexPage() { products = listOfProducts, country = country };
            if (listOfProducts.Any())
            {
                return View(model);
            }
            return View();
        }


    }
}
