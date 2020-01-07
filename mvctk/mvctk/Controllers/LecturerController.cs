using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvctk.Models;

namespace mvctk.Controllers
{
    public class LecturerController : Controller
    {
        private SQLContext DB = null;

        public LecturerController()
        {
            DB = new SQLContext();
        }


        public ActionResult Lecturer(user user)
        {
            Session["user"] = user.ID;
            return View(user);
        }

        public ActionResult Schedule()
        {
            List<course> Lecturer_courses = new List<course>();
            foreach (course c in DB.courses)
                if (c.LecturerID.Equals(Session["user"]))
                    Lecturer_courses.Add(c);

            return View(Lecturer_courses);

        }
        public ActionResult ExamSchedule()
        {
            List<course> Lecturer_courses = new List<course>();
            foreach (course c in DB.courses)
                if (c.LecturerID.Equals(Session["user"]))
                    Lecturer_courses.Add(c);

            return View(Lecturer_courses);

        }

        public ActionResult ShowStudents()
        {
            List<course> lecCourses = new List<course>();
            foreach (course c in DB.courses)
                if (c.LecturerID.Equals(Session["user"]))
                    lecCourses.Add(c);

            return View(lecCourses);
        }

        
        public ActionResult StudentsTable(string courseID)
        {
            List<string> studentsid = new List<string>();
            List<user> students = new List<user>();

            foreach (grade g in DB.grades)
                if (g.CourseID.Equals(courseID))
                    studentsid.Add(g.StudentID);


            foreach (String s in studentsid)
                students.Add(DB.users.Find(s));

            return View(students);
        }

        public ActionResult Show()
        {
           return RedirectToAction("show","grade");
        }



    }
}