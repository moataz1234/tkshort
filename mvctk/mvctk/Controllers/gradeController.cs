using mvctk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult Edit(string id1, string id2)
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
            var crs = DB.courses.FirstOrDefault(s => s.ID.Equals(grade.CourseID));
            DateTime datenow = DateTime.Now;
            DateTime ExamA = crs.ExamA;
            DateTime ExamB = crs.ExamB;

            int valueA = DateTime.Compare(ExamA, datenow);
            int valueB = DateTime.Compare(ExamB, datenow);

            // checking 
            if (valueA < 0)
            {
                ModelState.AddModelError("GradeA", "you can update the grade of moed A after the date of the exam");
            }
            if (valueB < 0)
            {
                ModelState.AddModelError("GradeB", "you can update the grade of moed B after the date of the exam");
            }
            else if (valueA > 0 || valueB>0 )
            {
                try
                {
                    var grad = DB.grades.FirstOrDefault(s => s.CourseID.Equals(grade.CourseID) && s.StudentID.Equals(grade.StudentID));
                    grad.CourseID = grade.CourseID;
                    grad.GradeA = grade.GradeA;
                    grad.GradeB = grade.GradeB;
                    grad.StudentID = grade.StudentID;
                    DB.SaveChanges();
                    return RedirectToAction("show");
                }
                catch (Exception ex)
                {
                }
            }
            return View(grade);
        }
        /*        protected override void Dispose(bool disposing)
                {
                    if (disposing)
                    {
                        DB.Dispose();
                    }
                    base.Dispose(disposing);
                }*/

        public ActionResult AddCourseToStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Submit(grade model)
        {


            if (ModelState.IsValid)
            {
                course temp1 = null;
                course temp2 = null;

                int flag = 0;
                List<course> courss = new List<course>();
                List<string> user_courses = new List<string>();
                string studentid = model.StudentID;
                string courseid = model.CourseID;

                foreach (grade g in DB.grades)
                    if (g.StudentID.Equals(studentid))
                        user_courses.Add(g.CourseID);
                foreach (string s in user_courses)
                {
                    temp1 = DB.courses.Find(s);
                    temp2 = DB.courses.Find(courseid);

                    if (temp1.Day.Equals(temp2.Day) && temp1.startlec.Equals(temp2.startlec) && temp1 != temp2)
                    {
                        flag = 1;
                    }
                }
                var crs = DB.courses.FirstOrDefault(s => s.ID.Equals(model.CourseID));
                var std = DB.users.FirstOrDefault(s => s.ID.Equals(model.StudentID));
                var grad = DB.grades.FirstOrDefault(s => s.CourseID.Equals(model.CourseID) && s.StudentID.Equals(model.StudentID));

                if (crs != null && std != null)
                {
                    if (grad == null)
                    {
                        if (flag == 0)
                        {
                         
                            if (std.UserTyper == 0)
                            {
                                DB.grades.Add(model);
                                DB.SaveChanges();
                                return RedirectToAction("AddCourseToStudent");
                            }
                            else
                                ModelState.AddModelError("StudentID", "student id is incorrect");
                        }
                        else
                            ModelState.AddModelError("CourseID", "There are conflict with onther course at the same time");

                    }
                    else
                        ModelState.AddModelError("CourseID", "Already having this Course");

                }
                else
                {
                    ModelState.AddModelError("StudentID", "The student id is incorrect");
                    ModelState.AddModelError("CourseID", "The course id is incorrect");

                }
            }
            else
                ModelState.AddModelError("StudentID", "The course id or student id is incorrect");
            return View("AddCourseToStudent", model);
        }
    }
}