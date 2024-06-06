using Microsoft.AspNetCore.Mvc;
using MinProject.Controllers.Objects;
using MinProject.Data;
using MinProject.ViewModel;

namespace MinProject.Controllers.Products
{
    public class StudentController : Controller
    {

        private readonly IStudentInterface _student;
        private readonly ApplicationDbContext _context;

        public StudentController(IStudentInterface student)
        {
            _student = student;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var students = await _student.GetStudents(pageNumber, pageSize);
            var totalStudent = await _student.GetTotalStudent();

            var viewModel = new ViewStudentModel() { Students = students, PageNumber = pageNumber, PageSize = pageSize, TotalStudents = totalStudent };
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> StudentList()
        {
            var students = _student.GetAllStudent();

            return Json(new { message = students.Result });
        }

        public async Task<IActionResult> StudentId()
        {
            return PartialView("_ProductsList");
        }
    }
}
//