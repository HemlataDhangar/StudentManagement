using Infrastructure.Contract;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;


namespace Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<T> dbSet;       
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            dbSet = unitOfWork.Db.Set<T>();
        }

        public IQueryable<T> FindAll()
        {
            
            return dbSet;
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public int Insert( T entity )
        {
            _unitOfWork.Db.Entry(entity).State = System.Data.Entity.EntityState.Added;
            _unitOfWork.Db.SaveChanges();

         
            return 1;
        }
                                
        public void ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            if (_unitOfWork.Db.Database.Connection.State == ConnectionState.Closed)
            {
                _unitOfWork.Db.Database.Connection.Open();
            }

            var command = _unitOfWork.Db.Database.Connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            command.ExecuteNonQuery();
        }

        public IQueryable ExcuteSqlQuery(string sqlQuery, CommandType commandType, SqlParameter[] parameters = null)
        {
            if (commandType == CommandType.Text)
            {
                return SqlQueryRun(sqlQuery, parameters).AsQueryable();
            }
            else if (commandType == CommandType.StoredProcedure)
            {
                return StoredProcedure(sqlQuery, parameters).AsQueryable();
            }

            return null;
        }

        private IQueryable SqlQueryRun(string sqlQuery, SqlParameter[] parameters = null)
        {
            if (parameters != null && parameters.Any())
            {
                var parameterNames = new string[parameters.Length];
                for (int i = 0; i<parameters.Length; i++)
                {
                    parameterNames[i] = parameters[i].ParameterName;
                }

                return  _unitOfWork.Db.Database.SqlQuery<T>(string.Format("{0}", sqlQuery, string.Join(",", parameterNames), parameters)).AsQueryable();
            }
            else
            {
                return _unitOfWork.Db.Database.SqlQuery<T>(sqlQuery).AsQueryable();
            }
        }

        private List<T> StoredProcedure(string storedProcedureName, SqlParameter[] parameters = null)
        {
            if (parameters != null &&parameters.Any())
            {
                var parameterNames = new string[parameters.Length];
                for (int i = 0; i<parameters.Length; i++)
                {
                    parameterNames[i] = parameters[i].SqlValue.ToString();
                }
                return _unitOfWork.Db.Database.SqlQuery<T>(string.Format("EXEC {0} {1}", storedProcedureName, string.Join(",", parameterNames))).ToList();
            }
            else
            {
                return _unitOfWork.Db.Database.SqlQuery<T>(string.Format("EXEC {0}", storedProcedureName)).ToList();
            }
        }
    }
}
