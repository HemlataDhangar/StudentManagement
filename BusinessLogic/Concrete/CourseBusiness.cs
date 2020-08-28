using System.Collections.Generic;
using BusinessLogic.Abstract.ICourse;
using Common.Model;
using DataAccess.Abstract.ICourseDL;

namespace BusinessLogic.Concrete
{
    public class CourseBusiness : ICourseBusiness
    {

        private readonly ICourseDataLayer _courseDataLayer;

        public CourseBusiness(ICourseDataLayer courseDataLayer)
        {
            _courseDataLayer = courseDataLayer;
        }

        public List<CourseModel> FindAll()
        {
            List<CourseModel> courseList = new List<CourseModel>();
            var x = _courseDataLayer.FindAll();

            foreach (var item in x)
            {
                CourseModel course = new CourseModel()
                {
                    CourseId = item.CourseId,
                    CourseName = item.CourseName
                };
                courseList.Add(course);
            }

            return courseList;
        }
    }
}
