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
           
            return View(user);
        }

        public ActionResult Schedule(user user)
        {
            List<@event> ev = new List<@event>();
             List<string> user_courses = new List<string>();
            foreach (grade g in DB.grades)
                if (g.StudentID == user.ID)
                    user_courses.Add(g.CourseID);


            foreach (String s in user_courses)
                foreach (course c in DB.courses)

                    if (s == c.ID)
                    {
                        ev.Add(new @event(c.Name, c.Time, c.ClassRoom));
                    }
                        
             return View(ev);

            //public ActionResult Schedual()
            //{

            //    return View(DB.users.ToList());
            //}
            public ActionResult ShowStudents()
            {

                return View(DB.users.ToList());
            }


        }
    }
}