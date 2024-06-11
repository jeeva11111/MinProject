using Microsoft.AspNetCore.Mvc;
using MinProject.Data;
using MinProject.Models.FormData;
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
        [HttpGet]
        public IActionResult CountryResult()
        {
            var students = _context.Students.ToList();

            return Json(new { response = students });
        }

        [HttpPost]
        public IActionResult DynamicFormITems([FromBody] List<DynamicDataModel> model)
        {
            if (model == null || !model.Any())
            {
                return BadRequest("Unable to read the data");
            }

            foreach (var item in model)
            {
                item.option = string.Join(",", item.optionts);
            }

            _context.DynamicDataModels.AddRange(model);
            _context.SaveChanges();

            return Ok();
        }

    }
}
