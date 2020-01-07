using mvctk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace mvctk.Controllers
{
    public class gradeController : Controller
    {
        private SQLContext DB = null;

        public gradeController()
        {
            DB = new SQLContext();
        }

        public ActionResult show()
        {
            return View(DB.grades);
        }

        public ActionResult Edit(string id1,string id2)
        {
            
            if (string.IsNullOrEmpty(id1) || string.IsNullOrEmpty(id2))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var grade = DB.grades.FirstOrDefault(x => x.StudentID.Equals(id1) && x.CourseID.Equals(id2));
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(grade grade)
        {

            try
            {
                var grad = DB.grades.First(s => s.CourseID.Equals(grade.CourseID) && s.StudentID.Equals(grade.StudentID));
                grad.CourseID = grade.CourseID;
                grad.GradeA = grade.GradeA;
                grad.GradeB = grade.GradeB;
                grad.StudentID = grade.StudentID;
                DB.SaveChanges();
                return RedirectToAction("show");
            }
            catch(Exception ex)
            {

            }
            
            return View(grade);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddCourseToStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Submit(grade model)
        {

            if (ModelState.IsValid)

            {

                DB.grades.Add(model);

                DB.SaveChanges();

                return RedirectToAction("AddCourseToStudent");

            }

            return View(model);
        }
    }
}