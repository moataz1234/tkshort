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

    }
}