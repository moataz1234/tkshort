using mvctk.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
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
            Session["user"] = user.ID;
            return View(user);
        }

        public ActionResult AddCourse()
        {
            //  Session["lecturers"] = DB.users;
            return View();
        }

        [HttpPost]
        public ActionResult Submit(course model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {

                    int flag = 0;
                    var cors = DB.courses.FirstOrDefault(s => s.ID.Equals(model.ID));
                    foreach (user u in DB.users)
                        if (model.LecturerID.Equals(u.ID) && u.UserTyper == 1)
                            flag = 1;
                    if (cors == null)
                    {
                        if (flag == 1)
                        {
                            DB.courses.Add(model);

                            DB.SaveChanges();

                            return RedirectToAction("Index");
                        }
                        else
                            ModelState.AddModelError("LecturerID", "The Lecturer does not exsit!");
                    }
                    else
                        ModelState.AddModelError("ID", "The Course Already exsit!");

                }
            }

            return View("AddCourse", model);
        }

        public ActionResult Index()
        {
            return View(DB.courses);
        }

        public ActionResult Delete(String id)
        {
            Session["ID"] = id;
            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            course course = DB.courses.Find(id);

            if (course == null)
            {

                return HttpNotFound();

            }

            return View(course);

        }



        [HttpPost, ActionName("Delete")]

        public ActionResult DelteConfirmed(string id)

        {

            course course = DB.courses.Find(id);

            DB.courses.Remove(course);

            DB.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Edit(string id)
        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            course course = DB.courses.Find(id);

            if (course == null)

            {

                return HttpNotFound();

            }

            return View(course);

        }

        [HttpPost]
        public ActionResult Edit(course course)
        {
            if (ModelState.IsValid)
            {
                int flag = 0;

                course temp2 = null;


                List<string> user_courses = new List<string>();
                List<string> courses = new List<string>();
                List<string> LecIds = new List<string>();
                foreach (grade g in DB.grades)
                    if (g.CourseID.Equals(course.ID))
                        user_courses.Add(g.StudentID);

                foreach (string s in user_courses)
                    foreach (grade g in DB.grades)
                        if (g.StudentID.Equals(s))
                            if (!g.CourseID.Equals(course.ID))
                                courses.Add(g.CourseID);

                int start, end, cstart, cend;
                cstart = Int32.Parse(course.startlec.Substring(0, 2));
                cend = Int32.Parse(course.endlec.Substring(0, 2));
                foreach (string s in courses)
                    foreach (course c in DB.courses)
                        if (s.Equals(c.ID))
                            if (c.Day.Equals(course.Day))
                            {
                                start = Int32.Parse(c.startlec.Substring(0, 2));
                                end = Int32.Parse(c.endlec.Substring(0, 2));
                                if (c.startlec.Equals(course.startlec) || (start > cend) || end > cstart)
                                    flag = 1;
                            }



                if (course != null && flag == 0)
                {
                    foreach (course c in DB.courses)
                        if (course.LecturerID.Equals(c.LecturerID))
                            if (!course.ID.Equals(c.ID))
                                if (c.Day.Equals(course.Day))
                                {
                                    start = Int32.Parse(c.startlec.Substring(0, 2));
                                    end = Int32.Parse(c.endlec.Substring(0, 2));
                                    if (c.startlec.Equals(course.startlec) || (start > cend) || end > cstart)
                                        flag = 1;
                                }
                    if (flag == 0)
                    {

                        DB.Entry(course).State = EntityState.Modified;
                        DB.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError("endlec", "There are conflict with lecturer's onther course at the same time");
                }
                else
                    ModelState.AddModelError("endlec", "There are conflict with student's onther course at the same time");

            }
            return View(course);
        }


        public ActionResult EditExam(string id)
        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            course course = DB.courses.Find(id);

            if (course == null)

            {

                return HttpNotFound();

            }

            return View("EditExam", course);

        }

        [HttpPost]
        public ActionResult EditExam(course course)
        {
            if (ModelState.IsValid)
            {
                //======first check==========
                DateTime ExamA = course.ExamA;
                DateTime ExamB = course.ExamB;
                int value = DateTime.Compare(ExamA, ExamB);
                //======second check=========
                int flag = 0, fa = 0, fb = 0;
                var lec = DB.users.FirstOrDefault(s => s.ID.Equals(course.LecturerID));
                List<DateTime> exama = new List<DateTime>();
                List<DateTime> examb = new List<DateTime>();

                foreach (course c in DB.courses)
                    if (c.LecturerID.Equals(course.LecturerID))
                    {
                        exama.Add(c.ExamA);
                        examb.Add(c.ExamB);
                    }
                foreach (DateTime a in exama)
                    foreach (DateTime b in examb)
                    {
                        if (a == course.ExamA)
                            fa = 1;
                        if (b == course.ExamB)
                            fb = 1;
                    }
                /*
                if (course.ExamB == course.ExamA)
                    flag = 1;*/
                if (fa == 0)
                {
                    if (fb == 0)
                    {

                        if (value < 0)
                        {
                            DB.Entry(course).State = EntityState.Modified;
                            DB.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                            ModelState.AddModelError("ExamA", "Moed A must be earlier than Moed B");
                    }
                    else
                        ModelState.AddModelError("ExamB", "the lecturer have an onther exam in this date");
                }
                else
                    ModelState.AddModelError("ExamA", "the lecturer have an onther exam in this date");


            }
            return View(course);

        }

    }
}