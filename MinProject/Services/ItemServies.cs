using Microsoft.EntityFrameworkCore;
using MinProject.Data;
using MinProject.Models.Student;

namespace MinProject.Services
{
    public class ItemServies : IItemServies
    {
        private readonly IItemRepository _itemRepository;
        public ItemServies(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task AddStudent(Student student)
        {

            await _itemRepository.AddStudent(student);
        }

        public async Task DeleteStudent(int Id)
        {

            await _itemRepository.DeleteStudent(Id);
        }

        public async Task<Student> GetStudent(int id)
        {

            return await _itemRepository.GetStudent(id);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {

            return await _itemRepository.GetStudents();
        }

        public async Task UpdateStudent(Student student)
        {

            await _itemRepository.UpdateStudent(student);
        }
    }

    public interface IItemServies
    {
        public Task<IEnumerable<Student>> GetStudents();
        public Task<Student> GetStudent(int id);
        public Task UpdateStudent(Student student);
        public Task AddStudent(Student student);
        public Task DeleteStudent(int Id);
    }


    public interface IItemRepository
    {
        public Task<IEnumerable<Student>> GetStudents();
        public Task<Student> GetStudent(int id);
        public Task UpdateStudent(Student student);
        public Task AddStudent(Student student);
        public Task DeleteStudent(int Id);
    }

    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(int Id)
        {

            var student = await _context.Students.FindAsync(Id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Student> GetStudent(int id)
        {

            return await _context.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {


            return await _context.Students.ToListAsync();
        }

        public async Task UpdateStudent(Student student)
        {

            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
    }
}
