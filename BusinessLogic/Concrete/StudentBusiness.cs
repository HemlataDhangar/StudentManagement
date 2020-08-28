using BusinessLogic.Abstract.IStudent;
using Common;
using Common.Model;
using DataAccess.Abstract.IStudentDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessLogic.Concrete
{
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IStudentDataLayer _studentDataLayer;

        public StudentBusiness(IStudentDataLayer studentDataLayer)
        {
            _studentDataLayer = studentDataLayer;
        }

        public List<StudentModel> FindAll()
        {

            //TODO: use Automapper             
            List<StudentModel> stuList = new List<StudentModel>();
            var x = _studentDataLayer.FindAll();

            foreach (var item in x)
            {
                StudentModel stu = new StudentModel()
                {
                    StudentId = item.StudentId,
                    Name = item.Name,
                    DateOfBirth = item.DateOfBirth.ToString(),
                    Age = Common.Helper.CalculateAge.GetAge((DateTime)item.DateOfBirth),
                    GenderType = item.Gender
                };
                stuList.Add(stu);
            }

            return stuList;

        }

        public StudentModel GetById(int id)
        {
            int paramCount = 1;
            SqlParameter[] sParams = new SqlParameter[paramCount];


            sParams[0] = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = id
            };

            var stuObject = _studentDataLayer.ExcuteSqlQuery("GetStudentById", CommandType.StoredProcedure, sParams).OfType<StudentModel>();

            var objModel = from t in stuObject
                           select new StudentModel()
                     {
                         StudentId = t.StudentId,
                         Name = t.Name,
                         DateOfBirth = t.DateOfBirth,
                         StudentGender = (Gender)Enum.Parse(typeof(Gender), t.GenderType),
                         CouseIds = t.CourseId.Split(',').Select(int.Parse).ToArray()
        };
            return objModel.OfType<StudentModel>().FirstOrDefault();
        }

        public void Insert(StudentModel model)
        {
            int paramCount = 5;
            SqlParameter[] sParams = new SqlParameter[paramCount];


            sParams[0] = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@StudentId",
                Value = model.StudentId
            };

            sParams[1] = new SqlParameter
            {
                SqlDbType = SqlDbType.VarChar,
                ParameterName = "@Name",
                Value = model.Name
            };

            sParams[2] = new SqlParameter
            {
                SqlDbType = SqlDbType.DateTime,
                ParameterName = "@DateofBirth",
                Value = Convert.ToDateTime(model.DateOfBirth)
            };

            sParams[3] = new SqlParameter
            {
                SqlDbType = SqlDbType.VarChar,
                ParameterName = "@Gender",
                Value = model.StudentGender
            };

            sParams[4] = new SqlParameter
            {
                SqlDbType = SqlDbType.VarChar,
                ParameterName = "@CourseIds",
                Value = model.CouseIds == null? string.Empty : string.Join(",", model.CouseIds)
            };
            _studentDataLayer.ExecuteNonQuery("ManageStudentWithCourse", CommandType.StoredProcedure, sParams);

        }

        public void Delete(int StudentId)
        {
            int paramCount = 1;
            SqlParameter[] sParams = new SqlParameter[paramCount];


            sParams[0] = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = StudentId
            };

            
            _studentDataLayer.ExecuteNonQuery("DeleteStudent", CommandType.StoredProcedure, sParams);

        }

    }
}
