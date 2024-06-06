using MinProject.Models.Student;

namespace MinProject.ViewModel
{
    public class ViewStudentModel
    {
        public IEnumerable<Student> Students { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
        public int TotalStudents { get; set; }
    }
}
