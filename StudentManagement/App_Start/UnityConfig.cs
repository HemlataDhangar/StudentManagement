using BusinessLogic.Abstract.ICourse;
using BusinessLogic.Abstract.IStudent;
using BusinessLogic.Concrete;
using DataAccess.Abstract.ICourseDL;
using DataAccess.Abstract.IStudentDL;
using DataAccess.Concrete.CourseDL;
using DataAccess.Concrete.StudentDL;
using Infrastructure;
using Infrastructure.Contract;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace StudentManagement
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            container.RegisterType<IStudentDataLayer, StudentDataLayer>();
            container.RegisterType<IStudentBusiness, StudentBusiness>();

            container.RegisterType<ICourseDataLayer, CourseDataLayer>();            
            container.RegisterType<ICourseBusiness, CourseBusiness>();            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}