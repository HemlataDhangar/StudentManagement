using BusinessLogic.Abstract.ICourse;
using BusinessLogic.Abstract.IStudent;
using Common.Model;
using System.Linq;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentBusiness _studentBusiness;
        private readonly ICourseBusiness _courseBusiness;
        public StudentController(IStudentBusiness studentBusiness, ICourseBusiness courseBusiness)
        {
            _studentBusiness = studentBusiness;
            _courseBusiness = courseBusiness;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetStudents()
        {
            return Json(_studentBusiness.FindAll(), JsonRequestBehavior.AllowGet);
        }

        public ViewResult ManageStudent(int studentid = 0)
        {
            var listCourse = _courseBusiness.FindAll();
            // Initialization.  
            SelectList lstobj = null;

            // Loading.  
            var list = listCourse.Select(p =>
                                        new SelectListItem
                                        {
                                            Value = p.CourseId.ToString(),
                                            Text = p.CourseName
                                        });

            // Setting.  
            lstobj = new SelectList(list, "Value", "Text");

            ViewBag.CourseList = lstobj;
            return View(_studentBusiness.GetById(studentid));
        }

        [HttpPost]
        public ActionResult ManageStudent(StudentModel studentModel)
        {

            _studentBusiness.Insert(studentModel);

            return RedirectToAction("Index", "Student");
        }

        [HttpPost]
        public ActionResult DeleteStudent(int studentid = 0)
        {
            _studentBusiness.Delete(studentid);
            return RedirectToAction("Index", "Student");           
        }
    }
}