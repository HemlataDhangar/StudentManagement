using Common.Model;
using System.Collections.Generic;

namespace BusinessLogic.Abstract.IStudent
{
    public interface IStudentBusiness 
    {
        void Insert(StudentModel studentModel);
        void Delete(int StudentId);
        StudentModel GetById(int id);

        List<StudentModel> FindAll();
    }
}
