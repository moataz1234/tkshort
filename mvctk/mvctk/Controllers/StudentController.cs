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

        public ActionResult ShowStudents()
        {

            return View(DB.users.ToList());
        }


    }
}