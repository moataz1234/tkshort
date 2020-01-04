using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvctk.Models;

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
        public ActionResult GetCoursesByJason()
        {
            return Json(DB.courses, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddCourse()
        {
            return View();
        }
    }
}