using Dapper;
using Microsoft.Data.SqlClient;
using MinProject.Data;
using MinProject.Models.Student;
using static MinProject.Controllers.Objects.StudentRepository;

namespace MinProject.Controllers.Objects
{
    public class StudentRepository : IStudentInterface
    {
        private readonly string _connectionString;
        private readonly ApplicationDbContext _context;
        public StudentRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            _connectionString = configuration.GetConnectionString("ServerLink");
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetStudents(int pageNumber, int pageSize)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                var SQlQuery = @"SELECT * FROM Students ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                var query = (from item in _context.Students orderby item.Id select item);
                var paginatedResult = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                return await connection.QueryAsync<Student>(SQlQuery, new { Offset = (pageNumber - 1) * pageSize, PageSize = pageSize });
            }
        }


        public async Task<int> GetTotalStudent()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT COUNT(1) FROM Students";

                return await connection.ExecuteScalarAsync<int>(sqlQuery);
            }
        }


        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            string? query = "SELECT * FROM Students";

            using (var connection = new SqlConnection(_connectionString))
            {

                return await connection.QueryAsync<Student>(query);

            }
        }

    }

    public interface IStudentInterface
    {
        public Task<IEnumerable<Student>> GetStudents(int pageNumber, int pageSize);
        public Task<IEnumerable<Student>> GetAllStudent();
        public Task<int> GetTotalStudent();


    }
}
