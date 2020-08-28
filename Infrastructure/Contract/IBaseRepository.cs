using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Contract
{
    public interface IBaseRepository<T> 
    {
        int Insert(T entity);
        T GetById(int id);
        IQueryable<T> FindAll();

        // 1. SqlQuery approach
        IQueryable ExcuteSqlQuery(string sqlQuery, CommandType commandType, SqlParameter[] parameters = null);

        // 2. SqlCommand approach
        void ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] parameters = null);
       
    }
}
