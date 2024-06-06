using Microsoft.Data.SqlClient;
using System.Text;

namespace MinProject.Controllers.GenerticController
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly string _tableName;
        private readonly string _connectionString;

        public GenericRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ServerLink");
            _tableName = typeof(T).Name;
        }

        public Task<int> Add(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var insertQuery = GenerateInsertQuery();
                // return await connection.ExecuteAsync(insertQuery, entity);

                return null;
            }
        }

        public Task<int> DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateEntity(int Id)
        {
            throw new NotImplementedException();
        }


        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName}");

            insertQuery.Append("(");

            var props = typeof(T).GetProperties().Where(x => x.Name != "Id").Select(x => x.Name);
            props.ToList().ForEach(prop => { insertQuery.Append($"[{prop}]"); });
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")VALUES(");

            props.ToList().ForEach(prop => { insertQuery.Append($"@{prop}"); });
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");

            return insertQuery.ToString();
        }
    }


    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetList();
        public Task<T> GetById(int Id);

        public Task<int> Add(T entity);
        public Task<int> DeleteById(int Id);
        public Task<int> UpdateEntity(int Id);
    }
}
