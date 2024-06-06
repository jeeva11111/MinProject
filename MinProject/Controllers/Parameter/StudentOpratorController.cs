using Microsoft.AspNetCore.Mvc;
using MinProject.Data;
using MinProject.Models.Customer;

namespace MinProject.Controllers.Parameter
{
    public class StudentOpratorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentOpratorController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View(GetCustomer(1));
        }
        [HttpPost]
        public IActionResult Index(int currentPageIndex)
        {
            return View(GetCustomer(currentPageIndex));
        }

        public IActionResult Result()
        {
            return View();
        }

        private CustomerModel GetCustomer(int currentPage)
        {
            int maxRows = 10;

            var model = new CustomerModel
            {
                Customers = _context.Customers
                    .OrderBy(c => c.Country)
                    .Skip((currentPage - 1) * maxRows)
                    .Take(maxRows)
                    .ToList(),
                PageCount = (int)Math.Ceiling((double)_context.Customers.Count() / maxRows),
                CurrentPageIndex = currentPage
            };

            return model;
        }

    }
}
