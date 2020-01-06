using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvctk.Models;

namespace mvctk.Controllers
{
    public class StudentController : Controller
    {
        private SQLContext DB = null;
        
        public StudentController()
        {
            DB = new SQLContext();
        }
        
        
        public ActionResult Student(user user)
        {
            Session["user"] = user.ID.ToString();
            return View(user);
        }

        public ActionResult Schedule()
        {
            
             List<string> user_courses = new List<string>();
            foreach (grade g in DB.grades)
                if (g.StudentID.Equals( Session["user"]))
                    user_courses.Add(g.CourseID);

                        List<course> crs = new List<course>();

            foreach (String s in user_courses)

                crs.Add(DB.courses.Find(s));

            return View(crs);

           
        }
        public ActionResult ExamSchedule()
        {
            List<string> user_courses = new List<string>();
            foreach (grade g in DB.grades)
                if (g.StudentID.Equals(Session["user"]))
                    user_courses.Add(g.CourseID);

            List<course> crs = new List<course>();

            foreach (String s in user_courses)

                crs.Add(DB.courses.Find(s));

            return View(crs);
            
        }
    }
}