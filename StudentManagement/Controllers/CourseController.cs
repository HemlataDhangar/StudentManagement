using BusinessLogic.Abstract.ICourse;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseBusiness _courseBusiness;
        public CourseController(ICourseBusiness courseBusiness)
        {
            _courseBusiness = courseBusiness;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetCourses()
        {
            return Json(_courseBusiness.FindAll(), JsonRequestBehavior.AllowGet);
        }
    }
}