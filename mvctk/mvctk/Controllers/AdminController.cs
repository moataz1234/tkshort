using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult Delete(int? id)
        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            course course = DB.courses.Find((id).ToString());

            if (course == null)
            {

                return HttpNotFound();

            }

            return View(course);

        }



        [HttpPost]//, ActionName("Delete")]

        public ActionResult DelteConfirmed(int id)

        {

            course course = DB.courses.Find(id.ToString());

            DB.courses.Remove(course);

            DB.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}