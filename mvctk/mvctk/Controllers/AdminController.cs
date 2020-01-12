using mvctk.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult Submit(course course)
        {
            if (ModelState.IsValid)
            {
                if (course != null)
                {

                    int flag = 0, classroomflag = 0;
                    var cors = DB.courses.FirstOrDefault(s => s.ID.Equals(course.ID));
                    //===================ckeck if this course conflict with other courses to the lecturer==========//
                    int start, end, cstart, cend;
                    int flag2 = 0;
                    cstart = Int32.Parse(course.startlec.Substring(0, 2));
                    cend = Int32.Parse(course.endlec.Substring(0, 2));
                    foreach (course c in DB.courses)
                        if (course.LecturerID.Equals(c.LecturerID))
                            if (!course.ID.Equals(c.ID))
                            {

                                if (c.Day.Equals(course.Day))
                                {
                                    start = Int32.Parse(c.startlec.Substring(0, 2));
                                    end = Int32.Parse(c.endlec.Substring(0, 2));
                                    if (c.startlec.Equals(course.startlec) || (start >= cstart && start < cend) || (end > cstart && end <= cend))
                                        flag2 = 1;
                                }
                                if (course.ClassRoom.Equals(c.ClassRoom))
                                    classroomflag = 1;
                            }
                    //======first check==========
                    DateTime ExamA = course.ExamA;
                    DateTime ExamB = course.ExamB;
                    int value = DateTime.Compare(ExamA, ExamB);
                    //======second check=========
                    int fa = 0, fb = 0;
                    var lec = DB.users.FirstOrDefault(s => s.ID.Equals(course.LecturerID));
                    List<DateTime> exama = new List<DateTime>();
                    List<DateTime> examb = new List<DateTime>();

                    foreach (course c in DB.courses)
                        if (course.LecturerID.Equals(c.LecturerID))
                            if (!course.ID.Equals(c.ID))
                            {
                                exama.Add(c.ExamA);
                                examb.Add(c.ExamB);
                            }
                    foreach (DateTime a in exama)
                        if (a == ExamA || a == ExamB)
                            fa = 1;
                    foreach (DateTime b in examb)
                        if (b == ExamB || b == ExamA)
                        {
                            fb = 1;
                        }
                    //=============================================================================================//
                    foreach (user u in DB.users)
                        if (course.LecturerID.Equals(u.ID) && u.UserTyper == 1)
                            flag = 1;
                    if (fa == 0)
                    {
                        if (fb == 0)
                        {

                            if (value > 0)
                            {

                                if (cors == null)
                                {
                                    if (flag == 1)
                                    {
                                        if (flag2 == 0)
                                        {
                                            if (classroomflag == 0)
                                            {

                                            DB.courses.Add(course);

                                            DB.SaveChanges();

                                            return RedirectToAction("Index");
                                            }
                                            else
                                                ModelState.AddModelError("ClassRoom", "There are another lecture in this Class");

                                        }
                                        else
                                            ModelState.AddModelError("LecturerID", "The Lecturer have onther course in this time,Please change lecturer or time");
                                    }
                                    else
                                        ModelState.AddModelError("LecturerID", "The Lecturer does not exsit!");
                                }
                                else
                                    ModelState.AddModelError("ID", "The Course Already exsit!");
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
            }

            return View("AddCourse", course);
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
                if (course != null)
                {
                    int flag1 = 0, flag2 = 0;

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
                                    if (c.startlec.Equals(course.startlec) || (start >= cstart && start < cend) || (end > cstart && end <= cend))
                                        flag1 = 1;
                                }


                    foreach (course c in DB.courses)
                        if (course.LecturerID.Equals(c.LecturerID))
                            if (!course.ID.Equals(c.ID))
                                if (c.Day.Equals(course.Day))
                                {
                                    start = Int32.Parse(c.startlec.Substring(0, 2));
                                    end = Int32.Parse(c.endlec.Substring(0, 2));
                                    if (c.startlec.Equals(course.startlec) || (start >= cstart && start < cend) || (end > cstart && end <= cend))
                                        flag2 = 1;
                                }
                    if (cend > cstart)
                    {

                        if (flag1 == 0)
                        {
                            if (flag2 == 0)
                            {

                                var cors = DB.courses.FirstOrDefault(s => s.ID.Equals(course.ID));
                                cors.ID = course.ID;
                                cors.Name = course.Name;
                                cors.Points = course.Points;
                                cors.LecturerID = course.LecturerID;
                                cors.ExamA = course.ExamA;
                                cors.ExamB = course.ExamB;
                                cors.startlec = course.startlec;
                                cors.endlec = course.endlec;
                                cors.Day = course.Day;
                                cors.ClassRoom = course.ClassRoom;
                                DB.SaveChanges();
                                return RedirectToAction("Index");
                            }
                            else
                                ModelState.AddModelError("endlec", "There are conflict with lecturer's in onther course at the same time");
                        }
                        else
                            ModelState.AddModelError("endlec", "There are conflict with student's in onther course at the same time");
                    }
                    else
                        ModelState.AddModelError("startlec", "the start lecture must be earlier than end lecture");

                }
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
        public ActionResult EditExam(course crs)
        {
            if (ModelState.IsValid)
            {
                //======first check==========
                DateTime ExamA = crs.ExamA;
                DateTime ExamB = crs.ExamB;
                int value = DateTime.Compare(ExamA, ExamB);
                //======second check=========
                int fa = 0, fb = 0;
                var lec = DB.users.FirstOrDefault(s => s.ID.Equals(crs.LecturerID));
                List<DateTime> exama = new List<DateTime>();
                List<DateTime> examb = new List<DateTime>();

                foreach (course c in DB.courses)
                    if (crs.LecturerID.Equals(c.LecturerID))
                        if (!crs.ID.Equals(c.ID))
                        {
                            exama.Add(c.ExamA);
                            examb.Add(c.ExamB);
                        }


                foreach (DateTime a in exama)
                    if (a == ExamA || a == ExamB)
                        fa = 1;
                foreach (DateTime b in examb)
                    if (b == ExamB || b == ExamA)
                    {
                        fb = 1;
                    }

                if (fa == 0)
                {
                    if (fb == 0)
                    {

                        if (value < 0)
                        {
                            try
                            {
                                var cors = DB.courses.FirstOrDefault(s => s.ID.Equals(crs.ID));
                                cors.ID = crs.ID;
                                cors.Name = crs.Name;
                                cors.Points = crs.Points;
                                cors.LecturerID = crs.LecturerID;
                                cors.ExamA = crs.ExamA;
                                cors.ExamB = crs.ExamB;
                                cors.startlec = crs.startlec;
                                cors.endlec = crs.endlec;
                                cors.Day = crs.Day;
                                cors.ClassRoom = crs.ClassRoom;
                                DB.SaveChanges();
                                return RedirectToAction("Index");
                            }
                            catch (Exception e)
                            {


                            }
                            // DB.Entry(crs).State = EntityState.Modified;
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
            return View(crs);

        }

    }
}