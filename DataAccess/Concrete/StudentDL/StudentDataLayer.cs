using Common.Model;
using DataAccess.Abstract.IStudentDL;
using Domain;
using Infrastructure.Contract;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Concrete.StudentDL
{
    public class StudentDataLayer : IStudentDataLayer
    {
        private readonly IBaseRepository<Student> _baseRepository;
        private readonly IBaseRepository<StudentModel> _baseRepositoryModel;
        public StudentDataLayer(IBaseRepository<Student> baseRepository, IBaseRepository<StudentModel> baseRepositoryModel) 
        {          
            _baseRepository = baseRepository;
            _baseRepositoryModel = baseRepositoryModel;
        }

        public int Insert(Student Entity)
        {
            return _baseRepository.Insert(Entity);
        }

        public Student GetById(int id) {

            return _baseRepository.GetById(id);
        }

        public IQueryable<Student> FindAll()
        {

            return _baseRepository.FindAll();
        }
        public void ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] parameters = null)
        {
            _baseRepository.ExecuteNonQuery(commandText, commandType, parameters);
        }
        public IQueryable ExcuteSqlQuery(string sqlQuery, CommandType commandType, SqlParameter[] parameters = null)
        {
            return _baseRepositoryModel.ExcuteSqlQuery(sqlQuery, commandType, parameters);
        }
    }
}
