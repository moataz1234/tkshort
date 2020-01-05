using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvctk.DAL;
using mvctk.Models;
using mvctk.ViewModel;
namespace mvctk.Controllers
{
    public class AdminController : Controller
    {
        private SQLContext DB = null;

        public AdminController()
        {
            DB = new SQLContext();
        }

        public ActionResult Admin(user user)
        {
            return View();
        }
        public ActionResult Index()
        {
            return View(DB.courses);
        }

        public ActionResult GetCoursesByJason()
        {
            CourseDal dal = new CourseDal();
            List<course> objCourses = dal.Courses.ToList<course>();
            return Json(objCourses, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddCourse()
        {
            /*   CourseDal dal = new CourseDal();
               List<course> objCourses = dal.Courses.ToList<course>();
               CourseViewModel cvm = new CourseViewModel();

               cvm.courses = new List<course>();
               cvm.courses = objCourses;
               return View(cvm);*/
            return View();
        }

        [HttpPost]
        public ActionResult Submit(course model)
        {
            var lecturerlist = DB.users.Where(x => x.UserTyper == 1).ToList();

            ViewData["lects"] = lecturerlist;

            if (ModelState.IsValid)

            {

                DB.courses.Add(model);

                DB.SaveChanges();

                return RedirectToAction("Index");

            }

            List<Object> list = new List<Object>();



            return View(model);
        }

       /* [HttpPost]
        public ActionResult Submit()
        {
            CourseViewModel cvm = new CourseViewModel();
            course objCourse = new course();
            objCourse.ID = Request.Form["course.ID"].ToString();
            objCourse.Name = Request.Form["course.Name"].ToString();
            objCourse.Points = Int32.Parse(Request.Form["course.Points"]);
            objCourse.ExamA = Request.Form["course.ExamB"].ToString();
            objCourse.ExamB = Request.Form["course.ExamB"].ToString();
            objCourse.LecturerID = Request.Form["course.LecturerID"].ToString();
            objCourse.Time = Request.Form["course.Time"].ToString();
            objCourse.ClassRoom = Request.Form["course.ClassRoom"].ToString();
            CourseDal dal = new CourseDal();


            dal.Courses.Add(objCourse);
            dal.SaveChanges();
            cvm.course = objCourse;

            cvm.courses = dal.Courses.ToList<course>();
            return View("AddCourse", cvm);
        }*/


    }
}