using Microsoft.AspNetCore.Mvc;
using MinProject.Models.Student;
using MinProject.Services;

namespace MinProject.Controllers.CrudOpractions
{
    public class ItemController : Controller
    {
        private readonly IItemServies _ItemServices;

        public ItemController(IItemServies IteamServices)
        {
            _ItemServices = IteamServices;
        }

        public async Task<IActionResult> Index()
        {
            var student = await _ItemServices.GetStudents();
            if (student != null) { return View(student); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            await _ItemServices.AddStudent(student);
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {
            await _ItemServices.UpdateStudent(student);
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            await _ItemServices.DeleteStudent(Id);
            return Json(new { success = true });
        }
        [HttpGet]
        public async Task<IActionResult> GetStudent(int Id)
        {
            var student = await _ItemServices.GetStudent(Id);
            return Json(student);
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _ItemServices.GetStudents();
            return Json(students);
        }

    }
}
