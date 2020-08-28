using Domain;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Abstract.IStudentDL
{
    public interface IStudentDataLayer 
    {
        int Insert(Student Entity);

        Student GetById(int id);

        IQueryable<Student> FindAll();

        void ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] parameters = null);
        IQueryable ExcuteSqlQuery(string sqlQuery, CommandType commandType, SqlParameter[] parameters = null);
    }
}
