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
        // GET: Lecturer
        public ActionResult Lecturer(user user)
        {
            return View();
        }
    }
}