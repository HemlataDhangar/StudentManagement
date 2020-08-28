using Common.Model;
using System.Collections.Generic;

namespace BusinessLogic.Abstract.ICourse
{
    public interface ICourseBusiness
    {
        List<CourseModel> FindAll();
    }
}
